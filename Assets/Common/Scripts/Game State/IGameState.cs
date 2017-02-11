public interface IGameState {

    // Callbacks
    void OnStateEnter(GameStateManager game);

    // Messages
    void Update(GameStateManager game);
}