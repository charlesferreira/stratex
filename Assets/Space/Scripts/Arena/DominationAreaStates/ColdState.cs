using UnityEngine;

namespace DominationAreaStates {

    public class ColdState : AbstractState {

        public ColdState(DominationArea dominationArea) : base(dominationArea) {
        }

        public override void OnStateEnter() {
            Debug.Log("Entering ColdState");

            var team = dominationArea.CurrentTeam;
            if (team == TeamFlags.Both) {
                ToOverheatedState();
                return;
            }

            if (team != TeamFlags.None) {
                ToWarmingUpState();
                return;
            }

            dominationArea.Color = dominationArea.coldColor;
        }

        public override void OnStateExit() {
            Debug.Log("Exiting ColdState");
        }

        public override void ShipHasEntered(TeamFlags team) {
            ToWarmingUpState();
        }

        public override void ShipHasLeft(TeamFlags team) {
            Debug.LogError("cold && ship left");
        }

        public override void Update() { }
    }
}
