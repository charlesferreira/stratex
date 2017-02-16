using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class TimeUpState : IGameState {
        
        public MessageInfo message;

        [Header("Zoom out")]
        [Range(0.1f, 1)]
        public float zoomOutScale;
        public float zoomOutSpeed;
        public Vector2 offset;

        public IEnumerator Play(GameStateManager game) {
            // pára o jogo
            game.TurnOffControls();
            game.stratex.TurnOff();
            SoundPlayer.StopAll();

            // foca o stratex lentamente com zoom out
            FocusArena(game);

            // exibe a mensagem de tempo esgotado
            ShowStartMessage();
            yield return new WaitForSeconds(message.fadeIn + message.slideIn + message.stay);

            // antes da mensagem sair, começa música de resultado
            SoundPlayer.PlayResultMusic();

            // aguarda a mensagem desaparecer para mudar de estado
            yield return new WaitForSeconds(message.fadeOut + message.slideOut);

            // oculta a mensagem
            HideStartMessage();

            game.ToEndingState();
        }

        void ShowStartMessage() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.TimeUp);
            messages.Show();
        }

        void HideStartMessage() {
            CommonMessages.Instance.Hide();
        }

        void FocusArena(GameStateManager game) {
            // faz as câmeras focarem o Stratex, com zoom
            game.shipCamera1.Zoom(zoomOutScale, zoomOutSpeed);
            game.shipCamera2.Zoom(zoomOutScale, zoomOutSpeed);
        }
    }
}