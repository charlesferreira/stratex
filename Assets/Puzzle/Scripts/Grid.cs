﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [Header("Grid dimensions")]
    [Range(1, 12)] public int rows = 6;
    [Range(1, 12)] public int columns = 6;
    [Range(0, 12)] public int startFullRows = 4;

    [Header("Block")]
    public GameObject blockPrefab;
    public Transform blocksContainer;
    public float randomStartSpacing = .2f;

    [Header("Textures")]
    public List<Sprite> blockSprites;

    [Header("Gizmos")]
    public bool drawLimits;
    public bool drawPoints;

    PuzzleToShipInterface ship;

    StatePatternBlock[,] grid;
    List<StatePatternBlock> matchingBlocks;

    private static Grid instance;
    public static Grid Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Grid>();
            }
            return instance;
        }
    }

    void Start()
    {
        ship = GetComponent<PuzzleToShipInterface>();
        StartGrid();
    }

    public void Update()
    {
        // TODO: Implementar lógica para verificar e descer os blocos somente quando necessário

        DecreaseBlocks();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InsertBlock(BlockColor.Blue);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InsertBlock(BlockColor.Green);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InsertBlock(BlockColor.Purple);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InsertBlock(BlockColor.Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            InsertBlock(BlockColor.Yellow);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            InsertBlock(BlockColor.Blue);
            InsertBlock(BlockColor.Green);
            InsertBlock(BlockColor.Purple);
            InsertBlock(BlockColor.Red);
            InsertBlock(BlockColor.Yellow);
        }
    }

    public bool InsertBlock(BlockColor blockColor)
    {
        var columnsNotFull = GetNotFullColumns();

        if (!columnsNotFull.Any())
            return false;

        var index = UnityEngine.Random.Range(0, columnsNotFull.Count - 1);
        var column = columnsNotFull.ElementAt(index);
        CreateNewBlock(column, rows - 1, blockColor, 0);
        return true;
    }

    List<int> GetNotFullColumns()
    {
        var list = new List<int>();
        for (int column = 0; column < columns; column++)
            if (!IsColumnFull(column))
                list.Add(column);
        return list;
    }

    private bool IsColumnFull(int column)
    {
        int blocks = 0;
        for (int row = 0; row < rows; row++)
            if (grid[column, row] != null)
                blocks++;

        if (blocks < rows)
            return false;
        return true;
    }

    private void StartGrid()
    {
        grid = new StatePatternBlock[columns, rows];

        for (int column = 0; column < columns; column++)
            for (int row = 0; row < Mathf.Min(startFullRows, rows); row++)
                CreateNewBlock(column, row, GetRandomValidColor(column, row), randomStartSpacing * (row + 1) + UnityEngine.Random.Range(0, Mathf.Ceil(randomStartSpacing * 1000)) / 1000);
    }

    private void CreateNewBlock(int column, int row, BlockColor color, float waitingTime)
    {
        var position = transform.position + GetGridCoord(new Vector3(column, rows, 0));
        var rotation = Quaternion.identity;
        var blockGO = Instantiate(blockPrefab, position, rotation, blocksContainer) as GameObject;

        var block = blockGO.GetComponent<StatePatternBlock>();

        if (grid[column, row] == null) {
            block.Init(column, row, color, waitingTime);
            grid[column, row] = block;
            return;
        }

        // Pode ocorrer que mais de um bloco tente entrar na mesma coluna, na mesma linha e no mesmo frame
        // Se isso ocorrer será procurado por uma linha vazia.
        // Sempre vai existir uma linha vazia, pois essa validação á feita na hora de escolher a coluna
        for (int newRow = rows - 2; newRow >= 0; newRow--) {
            if (grid[column, newRow] == null)
            {
                block.Init(column, newRow, color, waitingTime);
                block.transform.position += Vector3.down * 1f * (rows - newRow) + Vector3.back * (rows - newRow);
                grid[column, newRow] = block;
                return;
            }
        }
    }

    public void Swap(int columnA, int rowA, int columnB, int rowB)
    {
        StatePatternBlock blockA = grid[columnA, rowA];
        StatePatternBlock blockB = grid[columnB, rowB];

        if (blockA != null)
        {
            blockA.SwapToGridPosition(columnB, rowB);
        }
        if (blockB != null)
        {
            blockB.SwapToGridPosition(columnA, rowA);
        }

        grid[columnA, rowA] = blockB;
        grid[columnB, rowB] = blockA;
    }

    public void DecreaseBlock(int column, int row)
    {
        grid[column, row].Decrease();
        grid[column, row - 1] = grid[column, row];
        grid[column, row] = null;
    }

    internal bool IsEmptySpace(int column, int row)
    {
        if (column < 0 || column >= columns || row < 0 || row >= rows)
            return false;

        return grid[column, row] == null;
    }

    internal void CheckMatch(int column, int row)
    {
        var color = GetBlockColor(column, row);

        bool horizontal = CheckHorizontalMatch(color, column, row);
        bool vertical = CheckVerticalMatch(color, column, row);

        if (vertical || horizontal)
        {

            matchingBlocks = new List<StatePatternBlock>();

            grid[column, row].ToMatchingState();
            matchingBlocks.Add(grid[column, row]);

            int matchSize = 1;
            if (horizontal)
            {
                matchSize += DoHorizontalMatch(color, column, row);
            }
            if (vertical)
            {
                matchSize += DoVerticalMatch(color, column, row);
            }

            Debug.Log("Match " + color + ", size: " + matchSize);
            ship.NotifyMatch(new Match(color, matchSize));

            DestroyMatchingBlocks();
        }
    }

    private void DestroyMatchingBlocks()
    {

        foreach (var block in matchingBlocks)
        {
            Destroy(grid[block.Col, block.Row].gameObject);
            grid[block.Col, block.Row] = null;
        }
    }

    private void DecreaseBlocks()
    {
        for (int column = 0; column < columns; column++)
            for (int row = 0; row < rows; row++)
                if (grid[column, row] != null) {
                    if (grid[column, row].currentState.GetType() != typeof(FallingState)) {
                        if (IsEmptySpace(column, row - 1))
                            DecreaseBlock(column, row);
                    }
                }
    }

    private bool CheckHorizontalMatch(BlockColor color, int column, int row)
    {
        if (column > 0)
        {
            if (CheckIqualColors(column - 1, row, color))
            {
                // Esquerda
                if (column > 1)
                    if (CheckIqualColors(column - 2, row, color))
                        return true;

                // Centro
                if (column < columns - 1)
                    if (CheckIqualColors(column + 1, row, color))
                        return true;
            }
        }

        // Direita
        if (column < columns - 1)
            if (CheckIqualColors(column + 1, row, color))
                if (column < columns - 2)
                    if (CheckIqualColors(column + 2, row, color))
                        return true;

        return false;
    }

    private bool CheckVerticalMatch(BlockColor color, int column, int row)
    {
        if (row > 0)
        {
            if (CheckIqualColors(column, row - 1, color))
            {
                // Abaixo
                if (row > 1)
                    if (CheckIqualColors(column, row - 2, color))
                        return true;

                // Centro
                if (row < rows - 1)
                    if (CheckIqualColors(column, row + 1, color))
                        return true;
            }
        }

        // Acima
        if (row < rows - 1)
            if (CheckIqualColors(column, row + 1, color))
                if (row < rows - 2)
                    if (CheckIqualColors(column, row + 2, color))
                        return true;

        return false;
    }

    private int DoHorizontalMatch(BlockColor color, int column, int row)
    {
        int matchSize = 0;

        // Match à esquerda
        if (column > 0)
        {
            if (CheckIqualColors(column - 1, row, color))
            {
                grid[column - 1, row].ToMatchingState();
                matchingBlocks.Add(grid[column - 1, row]);
                matchSize += 1;

                if (column > 1)
                {
                    if (CheckIqualColors(column - 2, row, color))
                    {
                        grid[column - 2, row].ToMatchingState();
                        matchingBlocks.Add(grid[column - 2, row]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match à direita
        if (column < columns - 1)
        {
            if (CheckIqualColors(column + 1, row, color))
            {
                grid[column + 1, row].ToMatchingState();
                matchingBlocks.Add(grid[column + 1, row]);
                matchSize += 1;

                if (column < columns - 2)
                {
                    if (CheckIqualColors(column + 2, row, color))
                    {
                        grid[column + 2, row].ToMatchingState();
                        matchingBlocks.Add(grid[column + 2, row]);
                        matchSize += 1;
                    }
                }
            }
        }

        return matchSize;
    }

    private int DoVerticalMatch(BlockColor color, int column, int row)
    {
        int matchSize = 0;

        // Match abaixo
        if (row > 0)
        {
            if (CheckIqualColors(column, row - 1, color))
            {
                grid[column, row - 1].ToMatchingState();
                matchingBlocks.Add(grid[column, row - 1]);
                matchSize += 1;

                if (row > 1)
                {
                    if (CheckIqualColors(column, row - 2, color))
                    {
                        grid[column, row - 2].ToMatchingState();
                        matchingBlocks.Add(grid[column, row - 2]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match acima
        if (row < rows - 1)
        {
            if (CheckIqualColors(column, row + 1, color))
            {
                grid[column, row + 1].ToMatchingState();
                matchingBlocks.Add(grid[column, row + 1]);
                matchSize += 1;

                if (row < rows - 2)
                {
                    if (CheckIqualColors(column, row + 2, color))
                    {
                        grid[column, row + 2].ToMatchingState();
                        matchingBlocks.Add(grid[column, row + 2]);
                        matchSize += 1;
                    }
                }
            }
        }

        return matchSize;
    }

    private BlockColor GetBlockColor(int column, int row)
    {
        return grid[column, row].Color;
    }

    private bool CheckIqualColors(int column, int row, BlockColor color)
    {
        if (grid[column, row] == null)
        {
            return false;
        }
        return grid[column, row].Color == color;
    }

    private BlockColor GetRandomValidColor(int column, int row)
    {
        Array colorsTemp = Enum.GetValues(typeof(BlockColor));
        List<BlockColor> colors = new List<BlockColor>();
        foreach (var color in colorsTemp)
        {
            colors.Add((BlockColor)color);
        }

        while (colors.Count > 0)
        {
            var newColor = colors[UnityEngine.Random.Range(0, colors.Count)];

            if (CheckColorValidPosition(column, row, newColor))
            {
                return newColor;
            }
            else
            {
                colors.Remove(newColor);
            }
        }
        throw new Exception("Impossible to insert a new block");
    }

    private bool CheckColorValidPosition(int column, int row, BlockColor newColor)
    {
        // Se pode ocorrer combinação abaixo da peça
        if (row > 1)
        {
            if (grid[column, row - 1].Color == newColor)
            {
                if (grid[column, row - 2].Color == newColor)
                {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à esquerda
        if (column > 1)
        {
            if (grid[column - 1, row].Color == newColor)
            {
                if (grid[column - 2, row].Color == newColor)
                {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à direita
        if (column < columns - 2)
        {
            if (grid[column + 1, row] != null)
            {
                if (grid[column + 1, row].Color == newColor)
                {
                    if (grid[column + 2, row].Color == newColor)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public Sprite GetTexture(BlockColor color)
    {
        //TODO: ver com o Charles como criar um dicionário para melhorar isso.
        switch (color)
        {
            case BlockColor.Blue:
                return blockSprites[0];
            case BlockColor.Green:
                return blockSprites[1];
            case BlockColor.Purple:
                return blockSprites[2];
            case BlockColor.Red:
                return blockSprites[3];
            case BlockColor.Yellow:
                return blockSprites[4];
            default:
                throw new Exception("Invalid block color");
        }
    }

    public Vector3 GetGridCoord(Vector3 gridPosition)
    {
        return new Vector3(
            gridPosition.x * transform.localScale.x + 0.5f,
            gridPosition.y * transform.localScale.y + 0.5f,
            gridPosition.z);
    }

    void OnDrawGizmos()
    {
        if (drawLimits)
        {
            // Desenhar limites
            Gizmos.color = Color.white;
            Vector3 Point1 = transform.position + GetGridCoord(new Vector3(-.5f, -.5f, 0));
            Vector3 Point2 = transform.position + GetGridCoord(new Vector3(columns - 1 + .5f, -.5f, 0));
            Vector3 Point3 = transform.position + GetGridCoord(new Vector3(columns - 1 + .5f, rows - 1 + .5f, 0));
            Vector3 Point4 = transform.position + GetGridCoord(new Vector3(-.5f, rows - 1 + .5f, 0));

            Gizmos.DrawLine(Point1, Point2);
            Gizmos.DrawLine(Point2, Point3);
            Gizmos.DrawLine(Point3, Point4);
            Gizmos.DrawLine(Point4, Point1);
        }

        if (grid == null)
            return;

        if (drawPoints)
        {
            // Desenhar posições do grid
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    var size = 0f;
                    // Setando a cor e o tamanho
                    if (grid[column, row] == null)
                    {
                        size = 0.1f;
                        Gizmos.color = Color.gray;
                    }
                    else
                    {
                        size = grid[column, row].currentState == typeof(ActivegState) ? 0.2f : 0.1f;

                        switch (grid[column, row].Color)
                        {
                            case BlockColor.Blue:
                                Gizmos.color = Color.blue;
                                break;
                            case BlockColor.Green:
                                Gizmos.color = Color.green;
                                break;
                            case BlockColor.Purple:
                                Gizmos.color = Color.magenta;
                                break;
                            case BlockColor.Red:
                                Gizmos.color = Color.red;
                                break;
                            case BlockColor.Yellow:
                                Gizmos.color = Color.yellow;
                                break;
                            default:
                                Gizmos.color = Color.white;
                                break;
                        }
                    }
                    // Setando o tamanho conforme está ativo ou não
                    Gizmos.DrawSphere(transform.position + GetGridCoord(new Vector3(column, row, 0)), size);
                }
            }
        }
    }
}
