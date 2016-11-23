using UnityEngine;

namespace DominationAreaStates {

    public class CoolingDownState : AbstractState {

        float elapsedTime;
        Color startingColor;

        public CoolingDownState(DominationArea dominationArea) : base(dominationArea) {
        }

        public override void OnStateEnter() {
            Debug.Log("Entering CoolingDownState");
            elapsedTime = 0f;
            startingColor = dominationArea.Color;
        }

        public override void OnStateExit() {
            Debug.Log("Exiting CoolingDownState");
        }

        public override void ShipHasEntered(TeamFlags team) {
            if (dominationArea.CurrentTeam == TeamFlags.Both)
                ToOverheatedState();
        }

        public override void ShipHasLeft(TeamFlags team) {
        }

        public override void Update() {
            elapsedTime += Time.deltaTime;

            var rate = elapsedTime / dominationArea.TimeToCoolDown;
            var color = Color.Lerp(startingColor, dominationArea.coldColor, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToColdState();
            }
        }
    }
}
