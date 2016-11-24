using UnityEngine;

namespace DominationAreaStates {

    public class OverheatedState : AbstractState {

        public OverheatedState(DominationArea dominationArea) : base(dominationArea) { }

        public override void ShipHasEntered(TeamInfo team) {
            Debug.LogError("overheating && ship entered");
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToCoolingDownState();
        }

        public override void Update() {
            base.Update();

            var blink = Mathf.PingPong(elapsedTime, dominationArea.OverheatedBlinkTime) / dominationArea.OverheatedBlinkTime;
            var color = dominationArea.overheatedColorRange.Lerp(blink);
            dominationArea.Color = color;
        }
    }
}
