using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class ColdState : IDominationAreaState {

        public Color coldColor;

        public void OnStateEnter(DominationArea dominationArea) {
            dominationArea.StopSounds();

            // se tem duas naves, sobrecarrega
            var flag = dominationArea.CurrentTeam;
            if (flag == TeamFlags.Both) {
                dominationArea.ToOverheatedState();
                return;
            }

            // se tem uma nave, começa a aquecer
            if (flag == (TeamFlags.Team1 ^ TeamFlags.Team2)) {
                dominationArea.ToWarmingUpState(flag);
                return;
            }

            // redefine a velocidade do rotor e dos anéis
            dominationArea.rotor.ResetSpeed();
            dominationArea.rings.ResetSpeed();
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamFlags team) {
            dominationArea.ToWarmingUpState(team);
        }

        public void ShipHasLeft(DominationArea dominationArea, TeamFlags team) {
            Debug.LogError("cold && ship left");
        }

        public void Update(DominationArea dominationArea) {
            var damping = 0.01f;
            var color = Color.Lerp(dominationArea.Color, coldColor, damping);
            dominationArea.Color = color;
        }
    }
}
