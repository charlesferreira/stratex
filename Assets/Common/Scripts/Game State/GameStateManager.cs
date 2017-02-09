using GameStates;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    [HideInInspector] public IGameState startingState;
    [HideInInspector] public IGameState playingState;
    [HideInInspector] public IGameState scoringState;
    [HideInInspector] public IGameState restartingState;
    [HideInInspector] public IGameState endingState;
    [HideInInspector] public IGameState endedState;
    IGameState currentState;

    void Start () {
        startingState = new StartingState(this);
        SetState(startingState);
	}

    void Update () {
        currentState.Update();
	}

    void SetState(IGameState state) {
        if (currentState != null)
            currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }
}
