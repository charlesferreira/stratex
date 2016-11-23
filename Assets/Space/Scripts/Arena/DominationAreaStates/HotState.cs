using UnityEngine;

namespace DominationAreaStates {

    public class HotState : AbstractState {

        public HotState(DominationArea dominationArea) : base(dominationArea) {
        }

        public override void OnStateEnter() {
            Debug.Log("Entering HotState");
        }

        public override void OnStateExit() {
            Debug.Log("Exiting HotState");
        }

        public override void ShipHasEntered(TeamFlags team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamFlags team) {
            ToColdState();
        }

        public override void Update() { }
    }
}
