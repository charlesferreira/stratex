namespace GameStates {

    abstract public class AbstractState : IGameState {

        public virtual void ToStartingState() {
        }

        public virtual void ToPlayingState() {
        }

        public virtual void ToScoringState() {
        }

        public virtual void ToRestartingState() {
        }

        public virtual void ToEndingState() {
        }

        public virtual void ToEndedState() {
        }

        public virtual void OnStateEnter() {
        }

        public virtual void OnStateExit() {
        }

        public virtual void Update() {
        }
    }
}