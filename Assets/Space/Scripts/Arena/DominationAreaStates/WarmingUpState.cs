using UnityEngine;

namespace DominationAreaStates {

    public class WarmingUpState : AbstractState {

        Color startingColor;

        public WarmingUpState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            startingColor = dominationArea.Color;
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }

        public override void Update() {
            base.Update();

            var rate = elapsedTime / dominationArea.TimeToWarmUp;
            var color = Color.Lerp(startingColor, DominatingTeam.color, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToHotState(DominatingTeam);
            }
        }
    }
}
