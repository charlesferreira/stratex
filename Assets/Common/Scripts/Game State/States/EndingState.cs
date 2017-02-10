namespace GameStates {

    public class EndingState : IGameState {

        public void OnStateEnter(GameStateManager game) {
            // ativa os controles das naves e puzzles
            game.ship1.TurnOff();
            game.ship2.TurnOff();
            game.puzzle1.TurnOff();
            game.puzzle2.TurnOff();
        }

        public void OnStateExit(GameStateManager game) {
        }

        public void Update(GameStateManager game) {
        }
    }
}