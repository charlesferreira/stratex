using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public BlockInfo info;
    int column, row;

    public void Init(BlockInfo info)
    {
        this.info = info;
    }

	void Start () {
        //Instantiate(info.sprite, transform);
	}
	
	void Update () {
	
	}

    public void setGridPosition(int column, int row)
    {
        this.column = column;
        this.row = row;

        GetComponent<Movement>().MoveTo(Grid.Instance.GetGridCoord(new Vector3(column, row, 0)), (float)(Grid.Instance.rows - row) / (float)Grid.Instance.rows, (float)row / 3f + (Random.Range(0, 20) / 300f));
    }
}
