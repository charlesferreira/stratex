using UnityEngine;

namespace DominationAreaStates {

    public class HotState : AbstractState {
        
        private float timeToScoreNextPoint;

        public HotState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            Score();
            timeToScoreNextPoint = dominationArea.timeToWarmUp;
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }

        public override void Update() {
            base.Update();

            Debug.Log("Marcou um ponto!");
        }

        private void Score() {
            TeamsManager.Instance.Score(dominationArea.CurrentTeam);
        }
    }
}
