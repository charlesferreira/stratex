using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    [Header("Grid dimensions")]
    public int rows = 6;
    public int columns = 6;

    [Header("Block")]
    public GameObject block;
    public Transform blocksContainer;

    [Header("Textures")]
    public List<Sprite> blocksSprites;

    [Header("Gizmos")]
    public bool drawLimits;
    public bool drawPoints;

    public Transform[,] grid;

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
        grid = new Transform[columns, rows];

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var newBlock = Instantiate(block, GetGridCoord(new Vector3(x, rows, 0)), Quaternion.identity, blocksContainer) as GameObject;
                newBlock.GetComponent<SpriteRenderer>().sprite = blocksSprites[Random.Range(0, blocksSprites.Count - 1)];
                newBlock.GetComponent<Block>().setGridPosition(x, y);
            }
        }
    }

    void OnDrawGizmos()
    {

        if (drawLimits)
        {
            // Desenhar limites
            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetGridCoord(new Vector3(-0.5f, -0.5f, 0)), GetGridCoord(new Vector3(columns - 0.5f, -0.5f, 0)));
            Gizmos.DrawLine(GetGridCoord(new Vector3(columns - 0.5f, -0.5f, 0)), GetGridCoord(new Vector3(columns - 0.5f, rows - 0.5f, 0)));
            Gizmos.DrawLine(GetGridCoord(new Vector3(columns - 0.5f, rows - 0.5f, 0)), GetGridCoord(new Vector3(-0.5f, rows - 0.5f, 0)));
            Gizmos.DrawLine(GetGridCoord(new Vector3(-0.5f, rows - 0.5f, 0)), GetGridCoord(new Vector3(-0.5f, -0.5f, 0)));
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
                    if (grid[column, row] == null)
                    {
                        Gizmos.color = Color.white;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawSphere(GetGridCoord(new Vector3(column, row, 0)), 0.4f);
                }
            }
        }
    }

    public Vector3 GetGridCoord(Vector3 gridPosition)
    {
        return new Vector3(
            gridPosition.x * transform.localScale.x + transform.position.x + 0.5f,
            gridPosition.y * transform.localScale.y + transform.position.y + 0.5f,
            0);
    }
}
