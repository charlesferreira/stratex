using System;
using UnityEngine;

public class Cursor : MonoBehaviour {

    CursorState state;
    public int column, row;

	void Start () {

        state = CursorState.Inactive;
        StartPosition();
        Hide();
        
	}

    void Update () {
        switch (state)
        {
            case CursorState.Active:
                Active();
                break;
            case CursorState.Inactive:
                Inactive();
                break;
            default:
                break;
        }
    }

    internal void Move(CursorMovement direction)
    {
        switch (direction)
        {
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

    private void Inactive()
    {
        if (!Grid.Instance.grid[Grid.Instance.columns - 1, Grid.Instance.rows - 1].GetComponent<Movement>().IsMoving())
        {
            Show();
            state = CursorState.Active;
        }
    }

    private void Active()
    {
        
    }

    private void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void StartPosition()
    {
        column = (int)Mathf.Ceil(Grid.Instance.rows / 2) - 1;
        row = (int)Mathf.Ceil(Grid.Instance.rows / 2) - 1;

        transform.localPosition = Grid.Instance.GetGridCoord(new Vector3(column, row, -1));
    }
}
