using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class CoolingDownState : IDominationAreaState {

        [Range(0, 10)]
        public float timeToCoolDown;

        Color startingColor;
        float elapsedTime;
        float timeToUpdateRotorSpeed;

        public void OnStateEnter(DominationArea dominationArea) {
            elapsedTime = 0;
            timeToUpdateRotorSpeed = timeToCoolDown / (dominationArea.rotor.SpeedMultiplier + 1);
            startingColor = dominationArea.Color;
        }

        public void ShipHasEntered(DominationArea dominationArea, TeamInfo team) {
            if (dominationArea.CurrentTeam == TeamFlags.Both)
                dominationArea.ToOverheatedState();
        }

        public void ShipHasLeft(DominationArea dominationArea, TeamInfo team) { }

        public void Update(DominationArea dominationArea) {
            elapsedTime += Time.deltaTime;
            var rate = elapsedTime / timeToCoolDown;
            var color = Color.Lerp(startingColor, dominationArea.ColdColor, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                ToNextState(dominationArea);
            }
        }

        private void ToNextState(DominationArea dominationArea) {
            var flag = dominationArea.CurrentTeam;
            if (flag == TeamFlags.None)
                dominationArea.ToColdState();
            else
                dominationArea.ToWarmingUpState(TeamsManager.Instance.GetTeamInfo(flag));
        }

        void UpdateRotorSpeed(DominationArea dominationArea, float deltaTime) {
            if (elapsedTime % timeToUpdateRotorSpeed >= deltaTime)
                return;

            dominationArea.rotor.SlowDown();
            dominationArea.rings.SlowDown();
        }

        public void OnStateExit(DominationArea dominationArea) { }
    }
}
