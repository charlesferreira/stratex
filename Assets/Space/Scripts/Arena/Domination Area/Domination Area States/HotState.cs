namespace DominationAreaStates {

    [System.Serializable]
    public class HotState : IDominationAreaState {

        public GameStateManager gameStateManager;

        public void OnStateEnter(DominationArea dominationArea) {
            dominationArea.PlaySound(SoundPlayer.Instance.stratexHot);

            gameStateManager.ToScoringState(dominationArea.DominatingTeam);
            TeamsManager.Instance.Score(dominationArea.CurrentTeam);
        }

        public void OnStateExit(DominationArea dominationArea) { }

        public void ShipHasEntered(DominationArea dominationArea, TeamFlags team) { }

        public void ShipHasLeft(DominationArea dominationArea, TeamFlags team) { }

        public void Update(DominationArea dominationArea) { }
    }
}
