using UnityEngine;

public class Block : MonoBehaviour {

    public float swapSpeed = 0.2f;

    public int Column { get; set; }
    public int Row { get; set; }

    BlockColor color;
    BlockState state;

    public BlockColor Color { get { return color; } }
    public BlockState State { get { return state; } }

    Movement movement;

    public void Init(int column, int row, BlockColor color) {
        this.color = color;
        movement = GetComponent<Movement>();
        SetStartGridPosition(column, row);
        GetComponent<SpriteRenderer>().sprite = Grid.Instance.GetTexture(color);
    }

    void Update() {
        switch (state) {
            case BlockState.Entering:
                Entering();
                break;
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

    private void Entering() {
        if (!movement.IsMoving()) {
            state = BlockState.Active;
        }
    }

    private void Moving() {
        if (!movement.IsMoving()) {
            state = BlockState.Active;
            Grid.Instance.CheckMatch(Column, Row);
        }
    }

    void SetStartGridPosition(int column, int row) {
        Column = column;
        Row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));
        float duration = (float)(0 + Grid.Instance.rows - row) / (float)Grid.Instance.rows;
        float waitingTime = (float)row / 3f + (UnityEngine.Random.Range(0, 60) / 1000f);

        movement.MoveTo(target, duration, waitingTime);
        state = BlockState.Entering;
    }

    public void MoveToGridPosition() {
        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(Column, Row, 0));

        movement.MoveTo(target, swapSpeed);
        state = BlockState.Moving;
    }
    
    public void MoveToGridPosition(int column, int row) {
        Column = column;
        Row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(Column, Row, 0));

        movement.MoveTo(target, swapSpeed);
        state = BlockState.Moving;
    }

    public void MoveToTop(int column, int rows)
    {
        Column = column;
        Row = rows - 1;

        transform.localPosition = Grid.Instance.GetGridCoord(new Vector3(column, rows, 0));
    }

    public void SetMatching() {
        state = BlockState.Matching;
    }
}
