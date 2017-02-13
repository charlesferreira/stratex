using System.Collections;

public interface IGameState {
    IEnumerator Play(GameStateManager game);
}