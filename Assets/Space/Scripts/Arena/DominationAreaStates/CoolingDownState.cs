using UnityEngine;

namespace DominationAreaStates {

    public class CoolingDownState : AbstractState {

        Color startingColor;
        float timeToUpdateRotorSpeed;

        public CoolingDownState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            timeToUpdateRotorSpeed = dominationArea.timeToCoolDown / (dominationArea.rotor.SpeedMultiplier + 1);
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
            if (elapsedTime % timeToUpdateRotorSpeed >= deltaTime)
                return;
            
            dominationArea.rotor.SlowDown();
            dominationArea.rings.SlowDown();
        }
    }
}
