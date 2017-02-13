using System.Collections;

namespace GameStates {

    [System.Serializable]
    public class EndingState : IGameState {

        public IEnumerator Play(GameStateManager game) {
            yield return null;
        }
    }
}