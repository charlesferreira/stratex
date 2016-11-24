using UnityEngine;

namespace DominationAreaStates {

    public class ColdState : AbstractState {

        public ColdState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            var flag = dominationArea.CurrentTeam;
            if (flag == TeamFlags.Both) {
                ToOverheatedState();
                return;
            }

            if (flag == (TeamFlags.Team1 ^ TeamFlags.Team2)) {
                var team = TeamsManager.Instance.GetTeamInfo(flag);
                ToWarmingUpState(team);
                return;
            }

            dominationArea.Color = dominationArea.coldColor;
        }
        
        public override void ShipHasEntered(TeamInfo team) {
            ToWarmingUpState(team);
        }

        public override void ShipHasLeft(TeamInfo team) {
            Debug.LogError("cold && ship left");
        }

    }
}
