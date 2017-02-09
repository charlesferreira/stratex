using UnityEngine;

namespace GameStates {

    public class StartingState : IGameState {

        float elapsed;

        public void OnStateEnter(GameStateManager game) {
            elapsed = 0;

            // desativa os controles das naves e puzzles
            game.ship1.TurnOff();
            game.ship2.TurnOff();
            game.puzzle1.TurnOff();
            game.puzzle2.TurnOff();
        }

        public void OnStateExit(GameStateManager game) {
        }

        public void Update(GameStateManager game) {
            elapsed += Time.deltaTime;
            if (elapsed >= game.startingDuration)
                game.ToPlayingState();
        }
    }
}