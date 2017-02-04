using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform blocksContainer;

    PuzzleToShipInterface ship;

    StatePatternBlock[,] grid;
    List<StatePatternBlock> matchingBlocks;

    float comboSequence;

    void Awake()
    {
        grid = new StatePatternBlock[GridManager.Instance.columns, GridManager.Instance.rows];
    }

    void Start()
    {
        ship = GetComponent<PuzzleToShipInterface>();
    }

    public void Update()
    {
        DecreaseBlocks();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Eletric));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Fuel));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Missile));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Laser));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Shield));
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Eletric));
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Fuel));
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Missile));
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Laser));
            InsertBlock(PuzzlesManager.Instance.GetBlockInfo(BlockColor.Shield));
        }
    }

    public bool InsertBlock(BlockInfo blockInfo)
    {
        var columnsNotFull = GetNotFullColumns();

        if (!columnsNotFull.Any())
            return false;

        var index = UnityEngine.Random.Range(0, columnsNotFull.Count - 1);
        var column = columnsNotFull.ElementAt(index);
        CreateNewBlock(column, GridManager.Instance.rows - 1, blockInfo, 0);
        return true;
    }

    List<int> GetNotFullColumns()
    {
        var list = new List<int>();
        for (int column = 0; column < GridManager.Instance.columns; column++)
            if (!IsColumnFull(column))
                list.Add(column);
        return list;
    }

    private bool IsColumnFull(int column)
    {
        int blocks = 0;
        for (int row = 0; row < GridManager.Instance.rows; row++)
            if (grid[column, row] != null)
                blocks++;

        if (blocks < GridManager.Instance.rows)
            return false;
        return true;
    }

    public void CreateNewBlock(int column, int row, BlockInfo info, float waitingTime)
    {
        var position = transform.position + GetGridCoord(new Vector3(column, GridManager.Instance.rows, 0));
        var rotation = Quaternion.identity;
        var blockGO = Instantiate(GridManager.Instance.blockPrefab, position, rotation, blocksContainer) as GameObject;
        blockGO.GetComponent<StatePatternBlock>().Grid = this;

        var block = blockGO.GetComponent<StatePatternBlock>();

        if (grid[column, row] == null) {
            block.Init(column, row, info, waitingTime);
            grid[column, row] = block;
            return;
        }

        // Pode ocorrer que mais de um bloco tente entrar na mesma coluna, na mesma linha e no mesmo frame
        // Se isso ocorrer será procurado por uma linha vazia.
        // Sempre vai existir uma linha vazia, pois essa validação á feita na hora de escolher a coluna
        for (int newRow = GridManager.Instance.rows - 2; newRow >= 0; newRow--) {
            if (grid[column, newRow] == null)
            {
                block.Init(column, newRow, info, waitingTime);
                block.transform.position += Vector3.down * 1f * (GridManager.Instance.rows - newRow) + Vector3.back * (GridManager.Instance.rows - newRow);
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
            blockA.SwapToGridPosition(columnB, rowB);
        if (blockB != null)
            blockB.SwapToGridPosition(columnA, rowA);

        grid[columnA, rowA] = blockB;
        grid[columnB, rowB] = blockA;
    }

    internal bool IsEmptySpace(int column, int row)
    {
        if (column < 0 || column >= GridManager.Instance.columns || row < 0 || row >= GridManager.Instance.rows)
            return false;

        return grid[column, row] == null;
    }

    internal void CheckMatch(int column, int row)
    {
        var info = GetBlockInfo(column, row);

        bool horizontal = CheckHorizontalMatch(info, column, row);
        bool vertical = CheckVerticalMatch(info, column, row);

        if (vertical || horizontal)
        {

            matchingBlocks = new List<StatePatternBlock>();

            grid[column, row].ToMatchingState();
            matchingBlocks.Add(grid[column, row]);

            comboSequence = grid[column, row].comboSequence + 1;

            int matchSize = 1;
            if (horizontal)
            {
                matchSize += DoHorizontalMatch(info, column, row);
            }
            if (vertical)
            {
                matchSize += DoVerticalMatch(info, column, row);
            }

            //Debug.Log("Match " + info + ", size: " + matchSize + ", combo: " + comboSequence);
            ship.NotifyMatch(new Match(info, matchSize));

            DestroyMatchingBlocks();
        }
    }

    private void DestroyMatchingBlocks()
    {

        foreach (var block in matchingBlocks)
        {
            //Destroy(grid[block.Col, block.Row].gameObject);
            grid[block.Col, block.Row] = null;
        }

        foreach (var block in matchingBlocks)
        {
            InformMatchToColumnAboveRow(block.Col, block.Row);
        }
        comboSequence = 0;
    }

    private void DecreaseBlocks()
    {
        for (int column = 0; column < GridManager.Instance.columns; column++)
            for (int row = 0; row < GridManager.Instance.rows; row++)
                if (grid[column, row] != null) {
                    if (grid[column, row].currentState.GetType() != typeof(FallingState) && grid[column, row].currentState.GetType() != typeof(SwappingState)) {
                        if (IsEmptySpace(column, row - 1))
                            DecreaseBlock(column, row);
                    }
                }
    }

    public void DecreaseBlock(int col, int row)
    {
        grid[col, row].Decrease();
        grid[col, row - 1] = grid[col, row];
        grid[col, row] = null;
    }

    public void InformMatchToColumnAboveRow(int col, int row)
    {
        row++;
        while (row < GridManager.Instance.rows)
        {
            if (grid[col, row] != null)
            {
                grid[col, row].comboSequence = comboSequence;
            }
            else
                return;
            row++;
        }
    }

    private bool CheckHorizontalMatch(BlockInfo info, int column, int row)
    {
        if (column > 0)
        {
            if (CheckIqualColors(column - 1, row, info))
            {
                // Esquerda
                if (column > 1)
                    if (CheckIqualColors(column - 2, row, info))
                        return true;

                // Centro
                if (column < GridManager.Instance.columns - 1)
                    if (CheckIqualColors(column + 1, row, info))
                        return true;
            }
        }

        // Direita
        if (column < GridManager.Instance.columns - 1)
            if (CheckIqualColors(column + 1, row, info))
                if (column < GridManager.Instance.columns - 2)
                    if (CheckIqualColors(column + 2, row, info))
                        return true;

        return false;
    }

    private bool CheckVerticalMatch(BlockInfo info, int column, int row)
    {
        if (row > 0)
        {
            if (CheckIqualColors(column, row - 1, info))
            {
                // Abaixo
                if (row > 1)
                    if (CheckIqualColors(column, row - 2, info))
                        return true;

                // Centro
                if (row < GridManager.Instance.rows - 1)
                    if (CheckIqualColors(column, row + 1, info))
                        return true;
            }
        }

        // Acima
        if (row < GridManager.Instance.rows - 1)
            if (CheckIqualColors(column, row + 1, info))
                if (row < GridManager.Instance.rows - 2)
                    if (CheckIqualColors(column, row + 2, info))
                        return true;

        return false;
    }

    private int DoHorizontalMatch(BlockInfo info, int column, int row)
    {
        int matchSize = 0;

        // Match à esquerda
        if (column > 0)
        {
            if (CheckIqualColors(column - 1, row, info))
            {
                grid[column - 1, row].ToMatchingState();
                matchingBlocks.Add(grid[column - 1, row]);
                matchSize += 1;

                if (column > 1)
                {
                    if (CheckIqualColors(column - 2, row, info))
                    {
                        grid[column - 2, row].ToMatchingState();
                        matchingBlocks.Add(grid[column - 2, row]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match à direita
        if (column < GridManager.Instance.columns - 1)
        {
            if (CheckIqualColors(column + 1, row, info))
            {
                grid[column + 1, row].ToMatchingState();
                matchingBlocks.Add(grid[column + 1, row]);
                matchSize += 1;

                if (column < GridManager.Instance.columns - 2)
                {
                    if (CheckIqualColors(column + 2, row, info))
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

    private int DoVerticalMatch(BlockInfo info, int column, int row)
    {
        int matchSize = 0;

        // Match abaixo
        if (row > 0)
        {
            if (CheckIqualColors(column, row - 1, info))
            {
                grid[column, row - 1].ToMatchingState();
                matchingBlocks.Add(grid[column, row - 1]);
                matchSize += 1;

                if (row > 1)
                {
                    if (CheckIqualColors(column, row - 2, info))
                    {
                        grid[column, row - 2].ToMatchingState();
                        matchingBlocks.Add(grid[column, row - 2]);
                        matchSize += 1;
                    }
                }
            }
        }

        // Match acima
        if (row < GridManager.Instance.rows - 1)
        {
            if (CheckIqualColors(column, row + 1, info))
            {
                grid[column, row + 1].ToMatchingState();
                matchingBlocks.Add(grid[column, row + 1]);
                matchSize += 1;

                if (row < GridManager.Instance.rows - 2)
                {
                    if (CheckIqualColors(column, row + 2, info))
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

    private BlockInfo GetBlockInfo(int column, int row)
    {
        return grid[column, row].Info;
    }

    private bool CheckIqualColors(int column, int row, BlockInfo info)
    {
        if (grid[column, row] == null)
        {
            return false;
        }
        return grid[column, row].Info.color == info.color;
    }

    public BlockInfo GetRandomValidColor(int column, int row)
    {
        List<BlockInfo> infos = new List<BlockInfo>();
        foreach (var info in PuzzlesManager.Instance.blocksInfo)
        {
            infos.Add(info);
        }

        while (infos.Count > 0)
        {
            var randomInfo = infos[UnityEngine.Random.Range(0, infos.Count)];

            if (CheckColorValidPosition(column, row, randomInfo))
            {
                return randomInfo;
            }
            else
            {
                infos.Remove(randomInfo);
            }
        }
        throw new Exception("Impossible to insert a new block");
    }

    private bool CheckColorValidPosition(int column, int row, BlockInfo info)
    {
        // Se pode ocorrer combinação abaixo da peça
        if (row > 1)
        {
            if (grid[column, row - 1].Info == info)
            {
                if (grid[column, row - 2].Info == info)
                {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à esquerda
        if (column > 1)
        {
            if (grid[column - 1, row].Info == info)
            {
                if (grid[column - 2, row].Info == info)
                {
                    return false;
                }
            }
        }

        // Se pode ocorrer combinação à direita
        if (column < GridManager.Instance.columns - 2)
        {
            if (grid[column + 1, row] != null)
            {
                if (grid[column + 1, row].Info == info)
                {
                    if (grid[column + 2, row].Info == info)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
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
        if (GridManager.Instance.drawLimits)
        {
            // Desenhar limites
            Gizmos.color = Color.white;
            Vector3 Point1 = transform.position + GetGridCoord(new Vector3(-.5f, -.5f, 0));
            Vector3 Point2 = transform.position + GetGridCoord(new Vector3(GridManager.Instance.columns - 1 + .5f, -.5f, 0));
            Vector3 Point3 = transform.position + GetGridCoord(new Vector3(GridManager.Instance.columns - 1 + .5f, GridManager.Instance.rows - 1 + .5f, 0));
            Vector3 Point4 = transform.position + GetGridCoord(new Vector3(-.5f, GridManager.Instance.rows - 1 + .5f, 0));

            Gizmos.DrawLine(Point1, Point2);
            Gizmos.DrawLine(Point2, Point3);
            Gizmos.DrawLine(Point3, Point4);
            Gizmos.DrawLine(Point4, Point1);
        }

        if (grid == null)
            return;

        if (GridManager.Instance.drawPoints)
        {
            // Desenhar posições do grid
            for (int column = 0; column < GridManager.Instance.columns; column++)
            {
                for (int row = 0; row < GridManager.Instance.rows; row++)
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

                        switch (grid[column, row].Info.color)
                        {
                            case BlockColor.Eletric:
                                Gizmos.color = Color.blue;
                                break;
                            case BlockColor.Fuel:
                                Gizmos.color = Color.green;
                                break;
                            case BlockColor.Missile:
                                Gizmos.color = Color.yellow;
                                break;
                            case BlockColor.Laser:
                                Gizmos.color = Color.red;
                                break;
                            case BlockColor.Shield:
                                Gizmos.color = Color.cyan;
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
