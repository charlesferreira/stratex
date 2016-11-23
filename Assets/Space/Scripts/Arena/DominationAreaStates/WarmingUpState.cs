using UnityEngine;

namespace DominationAreaStates {

    public class WarmingUpState : AbstractState {

        float elapsedTime;
        Color startingColor;
        Color targetColor;

        public WarmingUpState(DominationArea dominationArea) : base(dominationArea) {
        }

        public override void OnStateEnter() {
            Debug.Log("Entering WarmingUpState");
            elapsedTime = 0f;
            startingColor = dominationArea.Color;

            var team = TeamsManager.Instance.GetTeam(dominationArea.CurrentTeam);
            targetColor = team.color;
        }

        public override void OnStateExit() {
            Debug.Log("Exiting WarmingUpState");
        }

        public override void ShipHasEntered(TeamFlags team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamFlags team) {
            ToColdState();
        }

        public override void Update() {
            elapsedTime += Time.deltaTime;

            var rate = elapsedTime / dominationArea.TimeToWarmUp;
            var color = Color.Lerp(startingColor, targetColor, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToHotState();
            }
        }
    }
}
