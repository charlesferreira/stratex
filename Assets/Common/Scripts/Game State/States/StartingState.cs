using UnityEngine;

namespace GameStates {

    public class StartingState : IGameState {

        float elapsed;

        public void OnStateEnter(GameStateManager game) {
            elapsed = 0;
            TurnOffControls(game);
            FocusStratex(game);
            HideHUD(game);
        }

        public void OnStateExit(GameStateManager game) {
        }

        public void Update(GameStateManager game) {
            elapsed += Time.deltaTime;
            if (elapsed >= game.startingDuration)
                game.ToPlayingState();
        }

        void TurnOffControls(GameStateManager game) {
            // desliga os controles das naves e dos puzzles
            game.ship1.TurnOff();
            game.ship2.TurnOff();
            game.puzzle1.TurnOff();
            game.puzzle2.TurnOff();
        }

        void FocusStratex(GameStateManager game) {
            // faz as câmeras focarem o Stratex, com zoom
            game.shipCamera1.SetTarget(game.dominationArea.transform).Zoom(2.5f, 0.15f);
            game.shipCamera2.SetTarget(game.dominationArea.transform).Zoom(2.5f, 0.15f);
        }

        void HideHUD(GameStateManager game) {
            // oculta a HUD, aumentando o zoom da câmera
            game.hudCamera1.Zoom(3, Mathf.Infinity);
            game.hudCamera2.Zoom(3, Mathf.Infinity);
        }
    }
}