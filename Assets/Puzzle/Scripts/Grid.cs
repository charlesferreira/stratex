using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    [Header("Grid dimensions")]
    public int rows = 10;
    public int columns = 10;

    public Transform[,] grid;

    private float halfScaleX;
    private float halfScaleY;

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

    void Start () {
        grid = new Transform[columns, rows];

        halfScaleX = transform.localScale.x / 2;
        halfScaleY = transform.localScale.y / 2;
    }

    void OnDrawGizmos()
    {
        // Desenhar limites
        Gizmos.color = Color.white;
        Gizmos.DrawLine(GetGridCoord(new Vector3(-halfScaleX, -halfScaleY, 0)), GetGridCoord(new Vector3(columns - halfScaleX, -halfScaleY, 0)));
        Gizmos.DrawLine(GetGridCoord(new Vector3(columns - halfScaleX, -halfScaleY, 0)), GetGridCoord(new Vector3(columns - halfScaleX, rows - halfScaleY, 0)));
        Gizmos.DrawLine(GetGridCoord(new Vector3(columns - halfScaleX, rows - halfScaleY, 0)), GetGridCoord(new Vector3(-halfScaleX, rows - halfScaleY, 0)));
        Gizmos.DrawLine(GetGridCoord(new Vector3(-halfScaleX, rows - halfScaleY, 0)), GetGridCoord(new Vector3(-halfScaleX, -halfScaleY, 0)));
        
        if (grid == null)
        {
            return;
        }
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

    public Vector3 GetGridCoord(Vector3 gridPosition)
    {
        return new Vector3(
            gridPosition.x * transform.localScale.x + transform.position.x + halfScaleX,
            gridPosition.y * transform.localScale.y + transform.position.y + halfScaleY
            , 0);
    }
}
