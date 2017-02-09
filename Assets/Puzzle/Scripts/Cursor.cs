using UnityEngine;

public class Cursor : MonoBehaviour {

    public int column, row;

    Grid grid;
    SpriteRenderer sprite;

    void Awake() {
        grid = GetComponentInParent<Grid>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start() {
        column = row = 0;
        transform.localPosition = grid.GetGridCoord(new Vector3(column, row, -1)) + new Vector3(.5f, 0, 0);
    }

    public void Move(CursorMovement direction) {
        if (!enabled) return;

        switch (direction) {
            case CursorMovement.Up:
                row++;
                break;
            case CursorMovement.Down:
                row--;
                break;
            case CursorMovement.Left:
                column--;
                break;
            case CursorMovement.Right:
                column++;
                break;
            default:
                break;
        }

        column = Mathf.Clamp(column, 0, GridManager.Instance.columns - 2);
        row = Mathf.Clamp(row, 0, GridManager.Instance.rows - 1);

        GetComponent<Movement>().MoveTo(grid.GetGridCoord(new Vector3(column, row, transform.position.z)) + new Vector3(.5f, 0, 0), .2f);
    }

    public void Swap(SwapDirection direction) {
        if (!enabled) return;

        switch (direction) {
            case SwapDirection.Up:
                if (row < GridManager.Instance.rows - 1) {
                    grid.Swap(column, row, column, row + 1);
                }
                break;
            case SwapDirection.Down:
                if (row > 0) {
                    grid.Swap(column, row, column, row - 1);
                }
                break;
            case SwapDirection.Left:
                if (column > 0) {
                    grid.Swap(column, row, column - 1, row);
                }
                break;
            case SwapDirection.Right:
                if (column < GridManager.Instance.columns - 1) {
                    grid.Swap(column, row, column + 1, row);
                }
                break;
            default:
                break;
        }
    }

    public void Show() {
        sprite.enabled = enabled = true;
    }

    public void Hide() {
        sprite.enabled = enabled = false;
    }
}
