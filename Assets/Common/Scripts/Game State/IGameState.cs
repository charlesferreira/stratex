using System.Collections;

public interface IGameState {

    // Callbacks
    IEnumerator Play(GameStateManager game);

    // Messages
    //void Update(GameStateManager game);
}