using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

    [Header("Grid dimensions")]
    [Range(1, 12)] public int rows = 6;
    [Range(1, 12)] public int columns = 6;
    [Range(0, 12)] public int startFullRows = 4;

    [Header("Block")]
    public GameObject blockPrefab;
    public float randomStartSpacing = .2f;

    [Header("Gizmos")]
    public bool drawLimits;
    public bool drawPoints;

    List<Grid> grids = new List<Grid>();

    static GridManager instance;
    public static GridManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GridManager>();
            return instance;
        }
    }

    void Start () {

        foreach (Transform child in transform)
            grids.Add(child.GetComponent<Grid>());

        StartGrids();
    }

    void Update () {
	
	}

    private void StartGrids()
    {
        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < Mathf.Min(startFullRows, rows); row++)
            {
                BlockInfo info = grids[0].GetRandomValidColor(column, row);
                foreach (Grid grid in grids)
                {
                    grid.CreateNewBlock(column, row, info, randomStartSpacing * (row + 1) + UnityEngine.Random.Range(0, Mathf.Ceil(randomStartSpacing * 1000)) / 1000);
                }
            }
        }
    }
}
