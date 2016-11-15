using UnityEngine;

public class Block : MonoBehaviour {

    public BlockInfo info;
    //int column, row;
    BlockColor color;
    BlockState state;
    public float swapSpeed = 0.2f;

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

    public void SetStartGridPosition(int column, int row)
    {
        //this.column = column;
        //this.row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));
        float duration = (float)(0 + Grid.Instance.rows - row) / (float)Grid.Instance.rows;
        float waitingTime = (float)row / 3f + (Random.Range(0, 60) / 1000f);

        GetComponent<Movement>().MoveTo(target, duration, waitingTime);
        state = BlockState.Moving;
    }

    public void SetGridPosition(int column, int row)
    {
        //this.column = column;
        //this.row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));

        GetComponent<Movement>().MoveTo(target, swapSpeed);
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
