using UnityEngine;

namespace DominationAreaStates {

    public class CoolingDownState : AbstractState {

        Color startingColor;
        float timeToUpdateRotorSpeed;

        public CoolingDownState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            startingColor = dominationArea.Color;
        }

        public override void ShipHasEntered(TeamInfo team) {
            if (dominationArea.CurrentTeam == TeamFlags.Both)
                ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) { }

        public override void Update() {
            base.Update();

            var rate = elapsedTime / dominationArea.timeToCoolDown;
            var color = Color.Lerp(startingColor, dominationArea.coldColor, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToNextState();
            }
        }

        private void ToNextState() {
            var flag = dominationArea.CurrentTeam;
            if (flag == TeamFlags.None)
                ToColdState();
            else
                ToWarmingUpState(TeamsManager.Instance.GetTeamInfo(flag));
        }

        void UpdateRotorSpeed(float deltaTime) {
            timeToUpdateRotorSpeed -= deltaTime;
            if (timeToUpdateRotorSpeed > 0) return;

            timeToUpdateRotorSpeed += dominationArea.timeToCoolDown / dominationArea.rings.frames;
            dominationArea.rotor.SlowDown();
        }
    }
}
