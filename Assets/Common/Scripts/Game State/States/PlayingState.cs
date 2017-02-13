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

        public IEnumerator Play(GameStateManager game) {
            // exibe mensagem de início ("Go!")
            ShowStartMessage();
            yield return new WaitForSeconds(messageFadeInDuration + messageStayDuration / 3);

            // habilita controles
            game.TurnOnControls();

            // inicia o timer, caso ainda não tenha sido iniciado
            GameTimer.Instance.Play();
            yield return new WaitForSeconds(messageStayDuration * 2 / 3);
            
            // oculta a mensagem de início
            HideStartMessage();

            // aguarda o fim da partida para mudar de estado
            yield return new WaitForSeconds(GameTimer.Instance.TimeLeft);
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