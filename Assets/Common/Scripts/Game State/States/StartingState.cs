using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class StartingState : IGameState {

        enum SubState { ZoomingIn, ShowingRoles }

        [Range(1, 5)]
        public float zoomIn;
        [Range(0, 5)]
        public float zoomInDuration;
        [Range(0, 5)]
        public float showRolesDuration;

        float elapsed;
        SubState substate;

        public void OnStateEnter(GameStateManager game) {
            elapsed = 0;
            substate = SubState.ZoomingIn;
            CommonMessages.Instance.SetMessage(CommonMessages.MessageType.None);
            ZoomIn(game);
        }

        public void Update(GameStateManager game) {
            elapsed += Time.deltaTime;
            switch (substate) {
                case SubState.ZoomingIn:
                    if (elapsed >= zoomInDuration) {
                        elapsed -= zoomInDuration;
                        substate = SubState.ShowingRoles;
                        ShowRoles(game);
                    }
                    break;
                case SubState.ShowingRoles:
                    if (elapsed >= showRolesDuration) {
                        game.ToPlayingState();
                    }
                    break;
            }
        }

        void ZoomIn(GameStateManager game) {
            // desliga os controles das naves e dos puzzles
            game.ship1.TurnOff();
            game.ship2.TurnOff();
            game.puzzle1.TurnOff();
            game.puzzle2.TurnOff();

            // faz as câmeras focarem o Stratex, com zoom
            game.shipCamera1.SetTarget(game.dominationArea.transform).Zoom(zoomIn, 0.15f);
            game.shipCamera2.SetTarget(game.dominationArea.transform).Zoom(zoomIn, 0.15f);

            // oculta a HUD, aumentando o zoom da câmera
            game.hudCamera1.Zoom(3);
            game.hudCamera2.Zoom(3);
        }

        void ShowRoles(GameStateManager game) {
            // retorna os focos das câmeras para as naves
            game.shipCamera1.ResetTarget().Zoom(1, 1);
            game.shipCamera2.ResetTarget().Zoom(1, 1);

            // exibe a HUD
            game.hudCamera1.Zoom(1, 1);
            game.hudCamera2.Zoom(1, 1);
        }
    }
}