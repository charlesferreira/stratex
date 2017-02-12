using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class ScoringState : IGameState {

        [Range(1, 5)]
        public float zoomIn;
        [Range(0, 5)]
        public float scoreFadeInDuration;
        [Range(0, 5)]
        public float scoreStayDuration;
        [Range(0, 5)]
        public float scoreFadeOutDuration;

        public IEnumerator Play(GameStateManager game) {
            // desliga os controles e foca no Stratex
            game.TurnOffControls();
            game.FocusStratex(zoomIn, 3);

            // exibe o placar
            ShowScoreboard();
            yield return new WaitForSeconds(scoreFadeInDuration + scoreStayDuration);

            // oculta o placar
            HideScoreboard();
            yield return new WaitForSeconds(scoreFadeOutDuration);

            // próximo estado
            game.ToRestartingState();
        }

        void ShowScoreboard() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Score);
            messages.Show(scoreFadeInDuration);
        }

        void HideScoreboard() {
            CommonMessages.Instance.Hide(scoreFadeOutDuration, new Color(1, 1, 1, 0));
        }
    }
}