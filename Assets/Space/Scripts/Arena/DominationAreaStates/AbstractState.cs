namespace DominationAreaStates {

    abstract public class AbstractState : IDominationAreaState {

        protected DominationArea dominationArea;

        public AbstractState(DominationArea dominationArea) {
            this.dominationArea = dominationArea;
        }

        public virtual void ToColdState() {
            dominationArea.SetState(dominationArea.coldState);
        }

        public virtual void ToWarmingUpState() {
            dominationArea.SetState(dominationArea.warmingUpState);
        }

        public virtual void ToHotState() {
            dominationArea.SetState(dominationArea.hotState);
        }

        public virtual void ToOverheatedState() {
            dominationArea.SetState(dominationArea.overheatedState);
        }

        public virtual void ToCoolingDownState() {
            dominationArea.SetState(dominationArea.coolingDownState);
        }

        public abstract void OnStateEnter();
        public abstract void OnStateExit();
        public abstract void ShipHasEntered(TeamFlags team);
        public abstract void ShipHasLeft(TeamFlags team);
        public abstract void Update();
    }
}
