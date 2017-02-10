namespace GameStates {

    public class PlayingState : IGameState {

        public void OnStateEnter(GameStateManager game) {
            TurnOnControls(game);
            FocusPlayerShip(game);
            ShowHUD(game);
        }

        public void OnStateExit(GameStateManager game) {
        }

        public void Update(GameStateManager game) {
        }

        void TurnOnControls(GameStateManager game) {
            // habilita os controles das naves e dos puzzles
            game.ship1.TurnOn();
            game.ship2.TurnOn();
            game.puzzle1.TurnOn();
            game.puzzle2.TurnOn();
        }

        void FocusPlayerShip(GameStateManager game) {
            // retorna os focos das câmeras para as naves
            game.shipCamera1.ResetTarget().Zoom(1, 1);
            game.shipCamera2.ResetTarget().Zoom(1, 1);
        }

        void ShowHUD(GameStateManager game) {
            // exibe a HUD
            game.hudCamera1.Zoom(1, 1);
            game.hudCamera2.Zoom(1, 1);
        }
    }
}