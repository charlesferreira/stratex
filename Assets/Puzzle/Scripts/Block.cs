using UnityEngine;

public class Block : MonoBehaviour {

    public BlockInfo info;
    //int column, row;
    BlockColor color;
    BlockState state;

    public void Init(BlockInfo info)
    {
        this.info = info;
    }

	void Start () {
        //Instantiate(info.sprite, transform);
	}
	
	void Update () {
        switch (state)
        {
            case BlockState.Active:
                break;
            case BlockState.Inactive:
                break;
            case BlockState.Moving:
                Moving();
                break;
            default:
                break;
        }
    }

    private void Moving()
    {
        if (!GetComponent<Movement>().IsMoving())
        {
            state = BlockState.Active;
        }
    }

    public void SetGridPosition(int column, int row)
    {
        //this.column = column;
        //this.row = row;

        GetComponent<Movement>().MoveTo(Grid.Instance.GetGridCoord(new Vector3(column, row, 0)), (float)(0 + Grid.Instance.rows - row) / (float)Grid.Instance.rows, (float)row / 3f + (Random.Range(0, 20) / 300f));
        state = BlockState.Moving;
    }

    public void SetColor(BlockColor color)
    {
        this.color = color;
    }

    public BlockColor GetColor()
    {
        return color;
    }

    public BlockState GetState()
    {
        return state;
    }
}
