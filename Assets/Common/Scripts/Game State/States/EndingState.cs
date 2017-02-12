using System.Collections;

namespace GameStates {

    [System.Serializable]
    public class EndingState : IGameState {

        public IEnumerator Play(GameStateManager game) {
            // ativa os controles das naves e puzzles
            game.ship1.TurnOff();
            game.ship2.TurnOff();
            game.puzzle1.TurnOff();
            game.puzzle2.TurnOff();

            yield return null;
        }
    }
}