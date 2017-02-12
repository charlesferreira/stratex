using System.Collections;

namespace GameStates {

    [System.Serializable]
    public class EndedState : IGameState {

        public IEnumerator Play(GameStateManager game) {
            yield return null;
        }
    }
}