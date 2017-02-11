using GameStates;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    // Public
    [Header("Debug")]
    public bool skipIntro;

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

    // States
    [Header("States")]
    [SerializeField] StartingState startingState;
    [SerializeField] PlayingState playingState;
    [SerializeField] ScoringState scoringState;
    [SerializeField] RestartingState restartingState;
    [SerializeField] EndingState endingState;
    [SerializeField] EndedState endedState;
    IGameState currentState;

    // Private
    TeamInfo scoringTeam;

    // Properties
    public TeamInfo ScoringTeam { get { return scoringTeam; } }

    // Methods
    void Start () {
        if (skipIntro)
            ToPlayingState();
        else
            ToStartingState();
    }

    void Update () {
        currentState.Update(this);
	}

    void SetState(IGameState state) {
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
