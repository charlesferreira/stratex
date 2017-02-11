using UnityEngine;

namespace DominationAreaStates {

    [System.Serializable]
    public class HotState : IDominationAreaState {

        public GameStateManager gameStateManager;

        public void OnStateEnter(DominationArea dominationArea) {
            gameStateManager.ToScoringState(dominationArea.DominatingTeam);
            TeamsManager.Instance.Score(dominationArea.CurrentTeam);
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamInfo team) {
            dominationArea.ToOverheatedState();
        }

        public void ShipHasLeft(DominationArea dominationArea, TeamInfo team) {
            dominationArea.ToColdState();
        }

        public void Update(DominationArea dominationArea) { }
    }
}
