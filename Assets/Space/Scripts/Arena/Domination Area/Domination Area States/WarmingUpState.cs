using UnityEngine;

namespace DominationAreaStates {

    public class WarmingUpState : AbstractState {

        Color startingColor;
        float timeToUpdateRotorSpeed;

        public WarmingUpState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            timeToUpdateRotorSpeed = dominationArea.timeToWarmUp / 10;
            startingColor = dominationArea.Color;

            dominationArea.rotor.ResetSpeed();
            dominationArea.rings.ResetSpeed();
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }

        public override void Update() {
            base.Update();

            var rate = elapsedTime / dominationArea.timeToWarmUp;
            var color = Color.Lerp(startingColor, DominatingTeam.color, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToHotState(DominatingTeam);
            }

            UpdateRotorSpeed(Time.deltaTime);
        }

        void UpdateRotorSpeed(float deltaTime) {
            if (elapsedTime % timeToUpdateRotorSpeed >= deltaTime)
                return;

            dominationArea.rotor.SpeedUp();
            dominationArea.rings.SpeedUp();
        }
    }
}
