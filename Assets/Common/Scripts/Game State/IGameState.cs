public interface IGameState {

    // Callbacks
    void OnStateEnter(GameStateManager game);
    void OnStateExit(GameStateManager game);

    // Messages
    void Update(GameStateManager game);
}