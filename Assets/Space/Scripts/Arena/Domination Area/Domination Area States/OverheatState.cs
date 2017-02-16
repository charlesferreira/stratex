using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class OverheatedState : IDominationAreaState {

        public ColorRange overheatedColorRange;
        [Range(0, 1)]
        public float overheatedBlinkTime;

        float elapsedTime;

        public void OnStateEnter(DominationArea dominationArea) {
            elapsedTime = 0;
            dominationArea.rotor.OverHeat();
            dominationArea.rings.OverHeat();
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamFlags team) { }

        public void ShipHasLeft(DominationArea dominationArea, TeamFlags team) {
            dominationArea.ToCoolingDownState();
        }

        public void Update(DominationArea dominationArea) {
            if (!SoundPlayer.Instance.stratexOverheat.isPlaying)
                dominationArea.PlaySound(SoundPlayer.Instance.stratexOverheat);

            elapsedTime += Time.deltaTime;
            var blink = Mathf.PingPong(elapsedTime, overheatedBlinkTime) / overheatedBlinkTime;
            var color = overheatedColorRange.Lerp(blink);
            dominationArea.Color = color;
        }
    }
}
