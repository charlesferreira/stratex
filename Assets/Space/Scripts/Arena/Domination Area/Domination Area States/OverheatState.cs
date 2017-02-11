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

        public void ShipHasEntered(DominationArea dominationArea, TeamInfo team) { }

        public void ShipHasLeft(DominationArea dominationArea, TeamInfo team) {
            dominationArea.ToCoolingDownState();
        }

        public void Update(DominationArea dominationArea) {
            elapsedTime += Time.deltaTime;
            var blink = Mathf.PingPong(elapsedTime, overheatedBlinkTime) / overheatedBlinkTime;
            var color = overheatedColorRange.Lerp(blink);
            dominationArea.Color = color;
        }
    }
}
