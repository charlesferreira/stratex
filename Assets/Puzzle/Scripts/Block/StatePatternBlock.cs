using UnityEngine;
using System.Collections;

public class StatePatternBlock : MonoBehaviour {

    Grid grid;

    public float swapDuration = 0.2f;

    [HideInInspector] public IBlockState currentState;

    [HideInInspector] public FallingState fallingState;
    [HideInInspector] public ActivegState activegState;
    [HideInInspector] public SwappingState swappingState;
    [HideInInspector] public MatchingState matchingState;

    [HideInInspector] public FreeFall freeFall;
    [HideInInspector] public Movement movement;


    [HideInInspector] public int Col;
    [HideInInspector] public int Row;

    BlockColor color;

    public BlockColor Color
    {
        get { return color; }
        set
        {
            color = value;
            GetComponent<SpriteRenderer>().sprite = PuzzlesManager.Instance.GetBlockInfo(color).puzzleSprite;
        }
    }

    public Grid Grid
    {
        get
        {
            return grid;
        }

        set
        {
            grid = value;
        }
    }

    private void Awake()
    {
        fallingState = new FallingState(this);
        activegState = new ActivegState(this);
        swappingState = new SwappingState(this);
        matchingState = new MatchingState();

        freeFall = GetComponent<FreeFall>();
        movement = GetComponent<Movement>();
        currentState = fallingState;
    }

    void Start () {
	}

    public void Init(int column, int row, BlockColor color, float waitingTime)
    {
        Col = column;
        Row = row;
        this.color = color;

        freeFall.ToFall(waitingTime);
        GetComponent<SpriteRenderer>().sprite = PuzzlesManager.Instance.GetBlockInfo(color).puzzleSprite;
    }

    void Update () {
        currentState.Update();
	}

    internal void Decrease()
    {
        currentState.Decrease();
    }

    public void SwapToGridPosition(int column, int row)
    {
        Col = column;
        Row = row;

        Vector3 target = Grid.GetGridCoord(new Vector3(Col, Row, 0));
        movement.MoveTo(target, swapDuration);

        currentState.ToSwappingState();
    }

    public void ToMatchingState()
    {
        currentState.ToMatchingState();
    }
}
