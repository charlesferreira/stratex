using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class PlayingState : IGameState {
        
        [Range(0, 2)]
        public float messageFadeInDuration;
        [Range(0, 2)]
        public float messageStayDuration;
        [Range(0, 2)]
        public float goFadeOut;

        public IEnumerator Play(GameStateManager game) {
            // exibe mensagem de início ("Go!")
            ShowStartMessage();
            yield return new WaitForSeconds(messageFadeInDuration + messageStayDuration / 3);

            // habilita controles
            game.TurnOnControls();
            yield return new WaitForSeconds(messageStayDuration * 2 / 3);
            
            // oculta a mensagem de início
            HideStartMessage();
        }

        void ShowStartMessage() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Go);
            messages.Show(messageFadeInDuration);
        }

        void HideStartMessage() {
            CommonMessages.Instance.Hide(goFadeOut, new Color(1, 1, 1, 0));
        }
    }
}