using UnityEngine;

namespace DominationAreaStates {

    abstract public class AbstractState : IDominationAreaState {

        protected TeamInfo DominatingTeam { get; private set; }

        protected DominationArea dominationArea;
        protected float elapsedTime;

        public AbstractState(DominationArea dominationArea) {
            this.dominationArea = dominationArea;
        }

        public void ToColdState() {
            dominationArea.SetState(dominationArea.coldState);
        }

        public void ToWarmingUpState(TeamInfo team) {
            var state = (WarmingUpState)dominationArea.warmingUpState;
            state.DominatingTeam = team;
            dominationArea.SetState(state);
        }

        public void ToHotState(TeamInfo team) {
            var state = (HotState)dominationArea.hotState;
            state.DominatingTeam = team;
            dominationArea.SetState(state);
        }

        public void ToOverheatedState() {
            dominationArea.SetState(dominationArea.overheatedState);
        }

        public void ToCoolingDownState() {
            dominationArea.SetState(dominationArea.coolingDownState);
        }

        public virtual void Update() {
            elapsedTime += Time.deltaTime;
        }

        public virtual void OnStateEnter() {
            elapsedTime = 0f;
        }

        public virtual void OnStateExit() {
        }

        public abstract void ShipHasEntered(TeamInfo team);
        public abstract void ShipHasLeft(TeamInfo team);

    }
}
