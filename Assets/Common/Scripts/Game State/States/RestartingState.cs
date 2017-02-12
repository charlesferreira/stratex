using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class RestartingState : IGameState {

        [Range(1, 5)]
        public float movingDuration;
        [Range(0, 1)]
        public float timeBeforeCoolDown;

        public IEnumerator Play(GameStateManager game) {
            // move o Stratex
            game.MoveStratex();
            yield return new WaitForSeconds(timeBeforeCoolDown);

            // esfria a área de dominação
            game.stratex.ToCoolingDownState();
            yield return new WaitForSeconds(movingDuration - timeBeforeCoolDown);

            // foca nos jogadores
            game.FocusPlayers();

            // próximo estado
            game.ToPlayingState();
        }
    }
}