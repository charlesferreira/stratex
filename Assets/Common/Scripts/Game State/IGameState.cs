public interface IGameState {

    // Transitions
    void ToStartingState();
    void ToPlayingState();
    void ToScoringState();
    void ToRestartingState();
    void ToEndingState();
    void ToEndedState();

    // Callbacks
    void OnStateEnter();
    void OnStateExit();

    // Messages
    void Update();
}