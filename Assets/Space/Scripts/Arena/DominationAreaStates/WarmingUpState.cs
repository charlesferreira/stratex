using UnityEngine;

namespace DominationAreaStates {

    public class WarmingUpState : AbstractState {

        Color startingColor;
        float timeToUpdateRotorSpeed;

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

            var rate = elapsedTime / dominationArea.timeToWarmUp;
            var color = Color.Lerp(startingColor, DominatingTeam.color, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToHotState(DominatingTeam);
            }

            UpdateRotorSpeed(Time.deltaTime);
        }

        void UpdateRotorSpeed(float deltaTime) {
            timeToUpdateRotorSpeed -= deltaTime;
            if (timeToUpdateRotorSpeed > 0) return;

            timeToUpdateRotorSpeed += dominationArea.timeToWarmUp / dominationArea.rings.frames;
            dominationArea.rotor.SpeedUp();
        }
    }
}
