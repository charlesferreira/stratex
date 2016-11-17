using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [Header("Grid dimensions")]
    public int rows = 6;
    public int columns = 6;

    [Header("Block")]
    public GameObject blockPrefab;
    public Transform blocksContainer;

    [Header("Textures")]
    public List<Sprite> blockSprites;

    [Header("Gizmos")]
    public bool drawLimits;
    public bool drawPoints;

    PuzzleToShipInterface ship;

    Block[,] grid;
    List<Block> matchingBlocks;

    private static Grid instance;
    public static Grid Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<Grid>();
            }
            return instance;
        }
    }

    public Block LastGridBlock { get { return grid[columns - 1, rows - 1]; } }

    void Start() {
        ship = GetComponent<PuzzleToShipInterface>();
        FillGrid();
    }

    private void FillGrid() {
        grid = new Block[columns, rows];

        for (int column = 0; column < columns; column++) {
            for (int row = 0; row < rows; row++) {
                CreateNewBlock(column, row);
            }
        }
    }

    private void CreateNewBlock(int column, int row) {
        var position = transform.position + GetGridCoord(new Vector3(column, rows, 0));
        var rotation = Quaternion.identity;
        var blockGO = Instantiate(blockPrefab, position, rotation, blocksContainer) as GameObject;

        var block = blockGO.GetComponent<Block>();
        var color = GetRandomValidColor(column, row);
        block.Init(column, row, color);
        grid[column, row] = block;
    }

    public void Swap(int columnA, int rowA, int columnB, int rowB) {
        Block blockA = grid[columnA, rowA];
        Block blockB = grid[columnB, rowB];

        // Só faz swap se os blocos estão ativos
        if (blockA.State == BlockState.Active || blockB.State == BlockState.Active) {
            blockA.SwapToGridPosition(columnB, rowB);
            blockB.SwapToGridPosition(columnA, rowA);

            grid[columnA, rowA] = blockB;
            grid[columnB, rowB] = blockA;
        }
    }

    internal void CheckMatch(int column, int row) {
        var color = GetBlockColor(column, row);

        bool horizontal = CheckHorizontalMatch(color, column, row);
        bool vertical = CheckVerticalMatch(color, column, row);

        if (vertical || horizontal) {

            matchingBlocks = new List<Block>();

            grid[column, row].SetMatching();
            matchingBlocks.Add(grid[column, row]);

            int matchSize = 1;
            if (horizontal) {
                matchSize += DoHorizontalMatch(color, column, row);
            }
            if (vertical) {
                matchSize += DoVerticalMatch(color, column, row);
            }

            Debug.Log("Match " + color + ", size: " + matchSize);
            ship.NotifyMatch(new Match(color, matchSize));

            DestroyMatchingBlocks();
        }
    }

    private void DestroyMatchingBlocks() {

        foreach (var block in matchingBlocks.OrderByDescending(x => x.Row))
        {
            DecraseColumnAboveRow(block.Column, block.Row);
        }
        foreach (var block in matchingBlocks.OrderByDescending(x => x.Row))
        {
            block.Color = GetRandomValidColor(block.Column, block.Row);
        }
    }

    private bool CheckHorizontalMatch(BlockColor color, int column, int row) {
        if (column > 0) {
            if (GetBlockColor(column - 1, row) == color) {
                // Esquerda
                if (column > 1)
                    if (GetBlockColor(column - 2, row) == color)
                        return true;

                // Centro
                if (column < columns - 1)
                    if (GetBlockColor(column + 1, row) == color)
                        return true;
            }
        }

        // Direita
        if (column < columns - 1)
            if (GetBlockColor(column + 1, row) == color)
                if (column < columns - 2)
                    if (GetBlockColor(column + 2, row) == color)
                        return true;

        return false;
    }

    private bool CheckVerticalMatch(BlockColor color, int column, int row) {
        if (row > 0) {
            if (GetBlockColor(column, row - 1) == color) {
                // Abaixo
                if (row > 1)
                    if (GetBlockColor(column, row - 2) == color)
                        return true;

                // Centro
                if (row < rows - 1)
                    if (GetBlockColor(column, row + 1) == color)
                        return true;
            }
        }

        // Acima
        if (row < rows - 1)
            if (GetBlockColor(column, row + 1) == color)
                if (row < rows - 2)
                    if (GetBlockColor(column, row + 2) == color)
                        return true;

        return false;
    }

    private int DoHorizontalMatch(BlockColor color, int column, int row) {
        int matchSize = 0;

        // Match à esquerda
        if (column > 0) {
            if (GetBlockColor(column - 1, row) == color) {
                grid[column - 1, row].SetMatching();
                matchingBlocks.Add(grid[column - 1, row]);
                matchSize += 1;

                if (column > 1) {
                    if (GetBlockColor(column - 2, row) == color) {
                        grid[column - 2, row].SetMatching();
                        matchingBlocks.Add(grid[column - 2, row]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match à direita
        if (column < columns - 1) {
            if (GetBlockColor(column + 1, row) == color) {
                grid[column + 1, row].SetMatching();
                matchingBlocks.Add(grid[column + 1, row]);
                matchSize += 1;

                if (column < columns - 2) {
                    if (GetBlockColor(column + 2, row) == color) {
                        grid[column + 2, row].SetMatching();
                        matchingBlocks.Add(grid[column + 2, row]);
                        matchSize += 1;
                    }
                }
            }
        }

        return matchSize;
    }

    private int DoVerticalMatch(BlockColor color, int column, int row) {
        int matchSize = 0;

        // Match abaixo
        if (row > 0) {
            if (GetBlockColor(column, row - 1) == color) {
                grid[column, row - 1].SetMatching();
                matchingBlocks.Add(grid[column, row - 1]);
                matchSize += 1;

                if (row > 1) {
                    if (GetBlockColor(column, row - 2) == color) {
                        grid[column, row - 2].SetMatching();
                        matchingBlocks.Add(grid[column, row - 2]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match acima
        if (row < rows - 1) {
            if (GetBlockColor(column, row + 1) == color) {
                grid[column, row + 1].SetMatching();
                matchingBlocks.Add(grid[column, row + 1]);
                matchSize += 1;

                if (row < rows - 2) {
                    if (GetBlockColor(column, row + 2) == color) {
                        grid[column, row + 2].SetMatching();
                        matchingBlocks.Add(grid[column, row + 2]);
                        matchSize += 1;
                    }
                }
            }
        }

        return matchSize;
    }

    private void DecraseColumnAboveRow(int column, int row) {
        // Guarda a referência do bloco atual
        Block tempBlock = grid[column, row];
        for (int rowFor = row; rowFor <= rows - 2; rowFor++) {
            // Desce todos os blocos acima
            grid[column, rowFor] = grid[column, rowFor + 1];
            grid[column, rowFor].Column = column;
            grid[column, rowFor].Row = rowFor;
            grid[column, rowFor].MoveToGridPosition();
        }
        // Coloca o bloco atual no topo
        grid[column, rows - 1] = tempBlock;
        tempBlock.MoveToTop(column, rows);
        tempBlock.MoveToGridPosition();
    }

    private BlockColor GetBlockColor(int column, int row) {
        return grid[column, row].Color;
    }

    private BlockColor GetRandomValidColor(int column, int row) {
        Array colorsTemp = Enum.GetValues(typeof(BlockColor));
        List<BlockColor> colors = new List<BlockColor>();
        foreach (var color in colorsTemp) {
            colors.Add((BlockColor)color);
        }

        while (colors.Count > 0) {
            var newColor = colors[UnityEngine.Random.Range(0, colors.Count)];

            if (CheckColorValidPosition(column, row, newColor)) {
                return newColor;
            }
            else {
                colors.Remove(newColor);
            }
        }
        throw new Exception("Impossible to insert a new block");
    }

    private bool CheckColorValidPosition(int column, int row, BlockColor newColor) {
        // Se pode ocorrer combinação abaixo da peça
        if (row > 1) {
            if (grid[column, row - 1].Color == newColor) {
                if (grid[column, row - 2].Color == newColor) {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à esquerda
        if (column > 1) {
            if (grid[column - 1, row].Color == newColor) {
                if (grid[column - 2, row].Color == newColor) {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à direita
        if (column < columns - 2) {
            if (grid[column + 1, row] != null) {
                if (grid[column + 1, row].Color == newColor) {
                    if (grid[column + 2, row].Color == newColor) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public Sprite GetTexture(BlockColor color) {
        //TODO: ver com o Charles como criar um dicionário para melhorar isso.
        switch (color) {
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

    public Vector3 GetGridCoord(Vector3 gridPosition) {
        return new Vector3(
            gridPosition.x * transform.localScale.x + 0.5f,
            gridPosition.y * transform.localScale.y + 0.5f,
            gridPosition.z);
    }

    void OnDrawGizmos() {
        if (drawLimits) {
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

        if (drawPoints) {
            // Desenhar posições do grid
            for (int column = 0; column < columns; column++) {
                for (int row = 0; row < rows; row++) {
                    // Setando a cor
                    switch (grid[column, row].Color) {
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

                    // Setando o tamanho conforme está ativo ou não
                    var tamanho = grid[column, row].State == BlockState.Active ? 0.2f : 0.1f;
                    Gizmos.DrawSphere(transform.position + GetGridCoord(new Vector3(column, row, 0)), tamanho);
                }
            }
        }
    }
}
