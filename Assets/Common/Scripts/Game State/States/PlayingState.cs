using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class PlayingState : IGameState {
        
        [Range(0, 5)]
        public float messageFadeInDuration;
        [Range(0, 2)]
        public float messageStayDuration;
        [Range(0, 2)]
        public float messageFadeOut;

        bool paused;

        public IEnumerator Play(GameStateManager game) {
            // caso esteja voltando da tela de pause, não exibe a mensagem "Go!"
            if (!paused) {
                ShowStartMessage();
                yield return new WaitForSeconds(messageFadeInDuration + messageStayDuration / 3);
            }

            // habilita controles e Stratex, se necessário
            game.TurnOnControls();
            game.stratex.SetIdle(false);

            // (re)inicia o timer
            GameTimer.Instance.Play();

            if (!paused) {
                yield return new WaitForSeconds(messageStayDuration * 2 / 3);
            
                // oculta a mensagem de início
                HideStartMessage();
            }
            paused = false;

            // aguarda o fim da partida ou Pause para mudar de estado
            PauseController.Instance.Resume();
            while (GameTimer.Instance.TimeLeft > 0) {
                if (PauseController.Instance.IsPaused) {
                    paused = true;
                    game.ToPausedState();
                }
                yield return new WaitForFixedUpdate();
            }
            game.ToTimeUpState();
        }

        void ShowStartMessage() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Go);
            messages.Show(messageFadeInDuration);
        }

        void HideStartMessage() {
            CommonMessages.Instance.Hide(messageFadeOut, new Color(1, 1, 1, 0));
        }
    }
}