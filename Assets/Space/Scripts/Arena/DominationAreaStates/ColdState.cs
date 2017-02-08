using UnityEngine;

namespace DominationAreaStates {

    public class ColdState : AbstractState {

        public ColdState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            // se tem duas naves, sobrecarrega
            var flag = dominationArea.CurrentTeam;
            if (flag == TeamFlags.Both) {
                ToOverheatedState();
                return;
            }

            // se tem uma nave, começa a aquecer
            if (flag == (TeamFlags.Team1 ^ TeamFlags.Team2)) {
                var team = TeamsManager.Instance.GetTeamInfo(flag);
                ToWarmingUpState(team);
                return;
            }

            // redefine a velocidade do rotor e dos anéis
            dominationArea.rotor.ResetSpeed();
            dominationArea.rings.ResetSpeed();
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToWarmingUpState(team);
        }

        public override void ShipHasLeft(TeamInfo team) {
            Debug.LogError("cold && ship left");
        }

        public override void Update() {
            base.Update();

            var damping = 0.01f;
            var color = Color.Lerp(dominationArea.Color, dominationArea.coldColor, damping);
            dominationArea.Color = color;
        }
    }
}
