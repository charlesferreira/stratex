using UnityEngine;
using System.Collections.Generic;
using System;

public class Grid : MonoBehaviour {

    [Header("Grid dimensions")]
    public int rows = 6;
    public int columns = 6;

    [Header("Block")]
    public GameObject block;
    public Transform blocksContainer;

    [Header("Textures")]
    public List<Sprite> blockSprites;

    [Header("Gizmos")]
    public bool drawLimits;
    public bool drawPoints;

    public GameObject[,] grid;

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

    void Start ()
    {
        StartGrid();
    }

    private void StartGrid()
    {
        FillGrid();
    }

    private void FillGrid()
    {
        grid = new GameObject[columns, rows];

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                var newBlock = Instantiate(block, transform.position + GetGridCoord(new Vector3(column, rows, 0)), Quaternion.identity, blocksContainer) as GameObject;
                var color = GetRandomValidColor(column, row);
                newBlock.GetComponent<Block>().SetColor(color);
                newBlock.GetComponent<SpriteRenderer>().sprite = GetTexture(color);
                newBlock.GetComponent<Block>().SetGridPosition(column, row);
                grid[column, row] = newBlock;
            }
        }
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
            var newColor = (BlockColor)colors[UnityEngine.Random.Range(0, colors.Count)];

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
        // Se pode acorrer combinação abaixo da peça
        if (row > 1)
        {
            if (grid[column, row - 1].GetComponent<Block>().GetColor() == newColor)
            {
                if (grid[column, row - 2].GetComponent<Block>().GetColor() == newColor)
                {
                    return false;
                }
            }
        }

        // Se pode acorrer combinação a esquerda
        if (column > 1)
        {
            if (grid[column - 1, row].GetComponent<Block>().GetColor() == newColor)
            {
                if (grid[column - 2, row].GetComponent<Block>().GetColor() == newColor)
                {
                    return false;
                }
            }
        }

        // Se pode acorrer combinação a direita
        if (column < columns - 2)
        {
            if (grid[column + 1, row] != null)
            {
                if (grid[column + 1, row].GetComponent<Block>().GetColor() == newColor)
                {
                    if (grid[column + 2, row].GetComponent<Block>().GetColor() == newColor)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private Sprite GetTexture(BlockColor color)
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
            gridPosition.x * transform.localScale.x + transform.localPosition.x + 0.5f,
            gridPosition.y * transform.localScale.y + transform.localPosition.y + 0.5f,
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
        {
            return;
        }
        if (drawPoints)
        {

            // Desenhar posições do grid
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    // Setando a cor
                    switch (grid[column, row].GetComponent<Block>().GetColor())
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

                    // Setando o tamanho conforme está ativo ou não
                    if (grid[column, row].GetComponent<Block>().GetState() == BlockState.Active)
                    {
                        Gizmos.DrawSphere(transform.position + GetGridCoord(new Vector3(column, row, 0)), 0.2f);
                    }
                    else
                    {
                        Gizmos.DrawSphere(transform.position + GetGridCoord(new Vector3(column, row, 0)), 0.1f);
                    }
                }
            }
        }
    }
}
