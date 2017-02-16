using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class WarmingUpState : IDominationAreaState {

        [Range(0, 10)]
        public float timeToWarmUp;

        [Range(1, 3)]
        public float maxPitch;

        Color startingColor;
        float elapsedTime;
        float timeToUpdateRotorSpeed;
        float basePitch;

        public void OnStateEnter(DominationArea dominationArea) {
            if (basePitch == 0)
                basePitch = SoundPlayer.Instance.stratexWarmingUp.pitch;
            SoundPlayer.Instance.stratexWarmingUp.pitch = basePitch;

            startingColor = dominationArea.Color;
            elapsedTime = 0;
            timeToUpdateRotorSpeed = timeToWarmUp / 10;

            dominationArea.rotor.ResetSpeed();
            dominationArea.rings.ResetSpeed();
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamFlags team) {
            dominationArea.ToOverheatedState();
        }

        public void ShipHasLeft(DominationArea dominationArea, TeamFlags team) {
            dominationArea.ToColdState();
        }

        public void Update(DominationArea dominationArea) {
            if (!SoundPlayer.Instance.stratexWarmingUp.isPlaying)
                dominationArea.PlaySound(SoundPlayer.Instance.stratexWarmingUp);

            elapsedTime += Time.deltaTime;
            var rate = elapsedTime / timeToWarmUp;
            var color = Color.Lerp(startingColor, dominationArea.DominatingTeam.stratexColor, rate);
            dominationArea.Color = color;

            SoundPlayer.Instance.stratexWarmingUp.pitch = basePitch + rate * (maxPitch - 1);

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
