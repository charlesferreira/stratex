using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class ScoringState : IGameState {

        public void OnStateEnter(GameStateManager game) {
            Debug.Log(game.ScoringTeam);
        }

        public void Update(GameStateManager game) {
        }
    }
}