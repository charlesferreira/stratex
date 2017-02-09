namespace GameStates {

    public class StartingState : AbstractState {
        private GameStateManager gameStateManager;

        public StartingState(GameStateManager gameStateManager) {
            this.gameStateManager = gameStateManager;
        }
    }
}