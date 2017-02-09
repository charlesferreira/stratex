namespace GameStates {

    public class PlayingState : IGameState {

        public void OnStateEnter(GameStateManager game) {
            // ativa os controles das naves e puzzles
            game.ship1.TurnOn();
            game.ship2.TurnOn();
            game.puzzle1.TurnOn();
            game.puzzle2.TurnOn();
        }

        public void OnStateExit(GameStateManager game) {
        }

        public void Update(GameStateManager game) {
        }
    }
}