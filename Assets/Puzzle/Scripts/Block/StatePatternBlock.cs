using UnityEngine;
using System.Collections;

public class StatePatternBlock : MonoBehaviour {

    public int Column { get; set; }
    public int Row { get; set; }

    BlockColor color;

    [HideInInspector] public IBlockState currentState;

    [HideInInspector] public EnteringState enteringState;
    [HideInInspector] public FallingState fallingState;
    [HideInInspector] public ActivegState activegState;
    [HideInInspector] public MovingState movingState;
    [HideInInspector] public MatchingState matchingState;

    private void Awake()
    {
        enteringState = new EnteringState(this);
        fallingState = new FallingState(this);
        activegState = new ActivegState(this);
        movingState = new MovingState(this);
        matchingState = new MatchingState(this);
    }

    void Start () {
        currentState = enteringState;
	}
	
	void Update () {
        currentState.Update();
	}
}
