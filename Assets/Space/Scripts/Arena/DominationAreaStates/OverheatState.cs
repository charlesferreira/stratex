using UnityEngine;

namespace DominationAreaStates {

    public class OverheatedState : AbstractState {

        float elapsedTime;

        public OverheatedState(DominationArea dominationArea) : base(dominationArea) {
        }

        public override void OnStateEnter() {
            Debug.Log("Entering OverheatState");
            elapsedTime = 0f;
        }

        public override void OnStateExit() {
            Debug.Log("Exiting OverheatState");
        }

        public override void ShipHasEntered(TeamFlags team) {
            Debug.LogError("overheat && ship entered");
        }

        public override void ShipHasLeft(TeamFlags team) {
            ToCoolingDownState();
        }

        public override void Update() {
            elapsedTime += Time.deltaTime;

            var t = Mathf.PingPong(elapsedTime, dominationArea.overheatedBlinkingTime);
            var color = dominationArea.overheatedColorRange.Lerp(t);
            dominationArea.Color = color;
        }
    }
}
