using UnityEngine;
using GameStates;

public class GameStateManager : MonoBehaviour {
    
    [Header("Debug")]
    public bool skipIntro;
    public bool skipGameplay;

    [Header("States")]
    [SerializeField] StartingState startingState;
    [SerializeField] PlayingState playingState;
    [SerializeField] ScoringState scoringState;
    [SerializeField] RestartingState restartingState;
    [SerializeField] TimeUpState timeUpState;
    [SerializeField] EndingState endingState;
    [SerializeField] PausedState pausedState;

    [Header("References")]
    public DominationArea stratex;
    public Transform stratexZoomFocus;
    public ShipOnOffSwitch ship1;
    public ShipOnOffSwitch ship2;
    public PuzzleOnOffSwitch puzzle1;
    public PuzzleOnOffSwitch puzzle2;
    public CameraMan shipCamera1;
    public CameraMan shipCamera2;
    public CameraMan hudCamera1;
    public CameraMan hudCamera2;

    // Private
    TeamInfo scoringTeam;
    DominationAreaMover stratexMover;

    // Properties
    public TeamInfo ScoringTeam { get { return scoringTeam; } }

    // Methods
    void Start () {
        stratexMover = stratex.GetComponent<DominationAreaMover>();

        if (skipGameplay)
            ToTimeUpState();
        else if (skipIntro)
            ToPlayingState();
        else
            ToStartingState();
    }

    void SetState(IGameState state) {
        StopAllCoroutines();
        StartCoroutine(state.Play(this));
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

    public void ToTimeUpState() {
        SetState(timeUpState);
    }

    public void ToEndingState() {
        SetState(endingState);
    }

    public void ToPausedState() {
        SetState(pausedState);
    }

    // Helpers
    public void TurnOffControls() {
        ship1.TurnOff();
        ship2.TurnOff();
        puzzle1.TurnOff();
        puzzle2.TurnOff();
    }

    public void TurnOnControls() {
        ship1.TurnOn();
        ship2.TurnOn();
        puzzle1.TurnOn();
        puzzle2.TurnOn();
    }

    public void Focus(Transform focus, float zoomScale, float hudZoomSpeed) {
        // faz as câmeras focarem o Stratex, com zoom
        shipCamera1.SetTarget(focus).Zoom(zoomScale, 0.15f);
        shipCamera2.SetTarget(focus).Zoom(zoomScale, 0.15f);

        // oculta a HUD, aumentando o zoom da câmera
        hudCamera1.Zoom(3, hudZoomSpeed);
        hudCamera2.Zoom(3, hudZoomSpeed);
    }

    public void Focus(Transform focus, float zoomScale) {
        Focus(focus, zoomScale, Mathf.Infinity);
    }

    public void FocusStratex(float zoomScale, float hudZoomSpeed) {
        Focus(stratex.transform, zoomScale, Mathf.Infinity);
    }

    public void FocusStratex(float zoomScale) {
        FocusStratex(zoomScale, Mathf.Infinity);
    }

    public void FocusPlayers() {
        // retorna os focos das câmeras para as naves
        shipCamera1.ResetTarget().Zoom(1, 1);
        shipCamera2.ResetTarget().Zoom(1, 1);

        // exibe a HUD
        hudCamera1.Zoom(1, 1);
        hudCamera2.Zoom(1, 1);
    }

    public void MoveStratex() {
        stratexMover.SelectNextPoint();
    }
}
