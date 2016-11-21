using UnityEngine;

public class Cursor : MonoBehaviour {

    public int column, row;

    void Start() {
        StartPosition();
    }

    void Update() {
        
    }

    public void Move(CursorMovement direction) {
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

        column = Mathf.Clamp(column, 0, Grid.Instance.columns - 1);
        row = Mathf.Clamp(row, 0, Grid.Instance.rows - 1);

        GetComponent<Movement>().MoveTo(Grid.Instance.GetGridCoord(new Vector3(column, row, transform.position.z)), .2f);
    }

    public void Swap(SwapDirection direction) {
        switch (direction) {
            case SwapDirection.Up:
                if (row < Grid.Instance.rows - 1) {
                    Grid.Instance.Swap(column, row, column, row + 1);
                }
                break;
            case SwapDirection.Down:
                if (row > 0) {
                    Grid.Instance.Swap(column, row, column, row - 1);
                }
                break;
            case SwapDirection.Left:
                if (column > 0) {
                    Grid.Instance.Swap(column, row, column - 1, row);
                }
                break;
            case SwapDirection.Right:
                if (column < Grid.Instance.columns - 1) {
                    Grid.Instance.Swap(column, row, column + 1, row);
                }
                break;
            default:
                break;
        }
    }

    void StartPosition() {
        column = 0;
        row = 0;

        transform.localPosition = Grid.Instance.GetGridCoord(new Vector3(column, row, -1));
    }
}
