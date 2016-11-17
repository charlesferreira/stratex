using UnityEngine;

public class Block : MonoBehaviour {

    public float swapSpeed = 0.2f;

    int column, row;
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
            Grid.Instance.CheckMatch(column, row);
        }
    }

    void SetStartGridPosition(int column, int row) {
        this.column = column;
        this.row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));
        float duration = (float)(0 + Grid.Instance.rows - row) / (float)Grid.Instance.rows;
        float waitingTime = (float)row / 3f + (UnityEngine.Random.Range(0, 60) / 1000f);

        movement.MoveTo(target, duration, waitingTime);
        state = BlockState.Entering;
    }

    public void SetGridPosition(int column, int row) {
        this.column = column;
        this.row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));

        movement.MoveTo(target, swapSpeed);
        state = BlockState.Moving;
    }

    public void SetMatching() {
        state = BlockState.Matching;
    }
}
