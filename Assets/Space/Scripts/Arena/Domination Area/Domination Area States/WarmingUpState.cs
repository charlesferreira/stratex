using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class WarmingUpState : IDominationAreaState {

        [Range(0, 10)]
        public float timeToWarmUp;

        Color startingColor;
        float elapsedTime;
        float timeToUpdateRotorSpeed;

        public void OnStateEnter(DominationArea dominationArea) {
            startingColor = dominationArea.Color;
            elapsedTime = 0;
            timeToUpdateRotorSpeed = timeToWarmUp / 10;

            dominationArea.rotor.ResetSpeed();
            dominationArea.rings.ResetSpeed();
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamInfo team) {
            dominationArea.ToOverheatedState();
        }

        public void ShipHasLeft(DominationArea dominationArea, TeamInfo team) {
            dominationArea.ToColdState();
        }

        public void Update(DominationArea dominationArea) {
            elapsedTime += Time.deltaTime;
            var rate = elapsedTime / timeToWarmUp;
            var color = Color.Lerp(startingColor, dominationArea.DominatingTeam.color, rate);
            dominationArea.Color = color;

            if (rate >= 1f) {
                dominationArea.ToHotState();
            }

            UpdateRotorSpeed(dominationArea, Time.deltaTime);
        }

        void UpdateRotorSpeed(DominationArea dominationArea, float deltaTime) {
            if (elapsedTime % timeToUpdateRotorSpeed >= deltaTime)
                return;

            dominationArea.rotor.SpeedUp();
            dominationArea.rings.SpeedUp();
        }
    }
}
