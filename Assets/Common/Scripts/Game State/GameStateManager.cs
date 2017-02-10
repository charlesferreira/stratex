using GameStates;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    // Public
    [Header("References")]
    public DominationArea dominationArea;
    public ShipOnOffSwitch ship1;
    public ShipOnOffSwitch ship2;
    public PuzzleOnOffSwitch puzzle1;
    public PuzzleOnOffSwitch puzzle2;
    public CameraMan shipCamera1;
    public CameraMan shipCamera2;
    public CameraMan hudCamera1;
    public CameraMan hudCamera2;

    [Header("Starting State")]
    [Range(0, 10)]
    public float startingDuration;

    // States
    IGameState startingState = new StartingState();
    IGameState playingState = new PlayingState();   
    IGameState scoringState = new ScoringState();   
    IGameState restartingState = new RestartingState();
    IGameState endingState = new EndingState();    
    IGameState endedState = new EndedState();     
    IGameState currentState;

    // Private
    TeamInfo scoringTeam;

    // Properties
    public TeamInfo ScoringTeam { get { return scoringTeam; } }

    // Methods
    void Start () {
        ToStartingState();
    }

    void Update () {
        currentState.Update(this);
	}

    void SetState(IGameState state) {
        if (currentState != null)
            currentState.OnStateExit(this);
        currentState = state;
        currentState.OnStateEnter(this);
    }

    // Transitions
    public void ToStartingState() {
        SetState(startingState);
    }

    public void ToPlayingState() {
        SetState(playingState);
    }

    public void ToScoringState(TeamInfo team) {
        scoringTeam = team;
        SetState(scoringState);
    }

    public void ToRestartingState() {
        SetState(restartingState);
    }

    public void ToEndingState() {
        SetState(endingState);
    }

    public void ToEndedState() {
        SetState(endedState);
    }
}
