using System;
using UnityEngine;

public class Block : MonoBehaviour {

    public float swapDuration = 0.2f;

    public int Column { get; set; }
    public int Row { get; set; }

    BlockColor color;
    BlockState state;

    public BlockColor Color {
        get { return color; }
        set {
            color = value;
            GetComponent<SpriteRenderer>().sprite = Grid.Instance.GetTexture(color);
        }
    }

    public BlockState State { get { return state; } }

    Movement movement;
    FreeFall freeFall;


    public void Init(int column, int row, BlockColor color, BlockState blockState) {

        Column = column;
        Row = row;

        this.color = color;
        movement = GetComponent<Movement>();
        freeFall = GetComponent<FreeFall>();
        GetComponent<SpriteRenderer>().sprite = Grid.Instance.GetTexture(color);
        if (blockState == BlockState.Entering) {

            Vector3 target = Grid.Instance.GetGridCoord(new Vector3(column, row, 0));
            float duration = (0 + Grid.Instance.rows - row) / (float)Grid.Instance.rows;
            float waitingTime = row / 3f + (UnityEngine.Random.Range(0, 60) / 1000f);
            movement.MoveTo(target, duration, waitingTime);
            state = BlockState.Entering;
        }
        else {
            ToFall();
        }
    }

    void Update() {
        switch (state)
        {
            case BlockState.Entering:
                Entering();
                break;
            case BlockState.Falling:
                Falling();
                break;
            case BlockState.Active:
                break;
            case BlockState.Moving:
                Moving();
                break;
            case BlockState.Matching:
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

            if (state == BlockState.Moving)
            {
                state = BlockState.Active;
                
                Grid.Instance.CheckMatch(Column, Row);
                return;
            }
            state = BlockState.Active;
        }
    }

    private void Falling()
    {
        var finalCoord = Grid.Instance.GetGridCoord(new Vector3(Column, Row, 0));
        
        if (finalCoord.y >= transform.localPosition.y)
        {
            if (Grid.Instance.IsEmptySpace(Column, Row - 1))
            {
                Grid.Instance.DecreaseBlock(Column, Row);
            }
            else
            {
                freeFall.Stop();
                transform.localPosition = finalCoord;
                state = BlockState.Active;
                Grid.Instance.CheckMatch(Column, Row);
            }
        }
    }

    internal void Decrease()
    {
        Row--;
        ToFall();
    }

    public void SwapToGridPosition(int column, int row)
    {
        Column = column;
        Row = row;

        Vector3 target = Grid.Instance.GetGridCoord(new Vector3(Column, Row, 0));
        movement.MoveTo(target, swapDuration);
        if (state == BlockState.Falling)
            freeFall.Stop();
        
        state = BlockState.Moving;
    }

    public void SetMatching() {
        state = BlockState.Matching;
    }

    public void ToFall()
    {
        state = BlockState.Falling;
        freeFall.ToFall();
    }
}
