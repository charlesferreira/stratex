using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class PlayingState : IGameState {

        public MessageInfo message;

        bool paused;

        public IEnumerator Play(GameStateManager game) {
            // caso esteja voltando da tela de pause, não exibe a mensagem "Go!"
            if (!paused) {
                ShowStartMessage();
                yield return new WaitForSeconds(message.fadeIn + message.slideIn);
            }

            // habilita controles e Stratex, se necessário
            game.TurnOnControls();
            game.stratex.SetIdle(false);

            // (re)inicia o timer
            GameTimer.Instance.Play();

            if (!paused) {
                yield return new WaitForSeconds(message.stay + message.slideOut + message.fadeOut);
            
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
            messages.Show();
        }

        void HideStartMessage() {
            CommonMessages.Instance.Hide();
        }
    }
}