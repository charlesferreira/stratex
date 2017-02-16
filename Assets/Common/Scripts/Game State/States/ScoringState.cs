using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class ScoringState : IGameState {

        public MessageInfo message;
        [Range(1, 5)]
        public float zoomIn;


        public IEnumerator Play(GameStateManager game) {
            // desliga os controles e foca no Stratex
            game.TurnOffControls();
            game.FocusStratex(zoomIn, 3);

            // exibe o placar
            ShowScoreboard();
            yield return new WaitForSeconds(message.TotalDuration);
            HideScoreboard();

            // próximo estado
            game.ToRestartingState();
        }

        void ShowScoreboard() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Score);
            messages.Show();
        }

        void HideScoreboard() {
            CommonMessages.Instance.Hide();
        }
    }
}