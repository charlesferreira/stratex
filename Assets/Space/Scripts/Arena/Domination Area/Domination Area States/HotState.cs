using UnityEngine;

namespace DominationAreaStates {

    public class HotState : AbstractState {

        public HotState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            Debug.Log("Marcou um ponto!");

            Score();
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }

        public override void Update() {
            base.Update();
        }

        private void Score() {
            TeamsManager.Instance.Score(dominationArea.CurrentTeam);
        }
    }
}
