using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class TimeUpState : IGameState {

        [Header("Message")]
        [Range(0, 5)]
        public float messageFadeInDuration;
        [Range(0, 5)]
        public float messageStayDuration;
        [Range(0, 5)]
        public float messageFadeOutDuration;

        [Header("Zoom out")]
        [Range(0.1f, 1)]
        public float zoomOutScale;
        public float zoomOutSpeed;
        public Vector2 offset;

        public IEnumerator Play(GameStateManager game) {
            // pára o jogo
            game.TurnOffControls();
            game.stratex.TurnOff();

            // foca o stratex lentamente com zoom out
            FocusArena(game);

            // exibe a mensagem de tempo esgotado
            ShowStartMessage();
            yield return new WaitForSeconds(messageFadeInDuration + messageStayDuration);

            // oculta a mensagem
            HideStartMessage();

            // aguarda a mensagem desaparecer para mudar de estado
            yield return new WaitForSeconds(messageFadeOutDuration);
            game.ToEndingState();
        }

        void ShowStartMessage() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.TimeUp);
            messages.Show(messageFadeInDuration);
        }

        void HideStartMessage() {
            CommonMessages.Instance.Hide(messageFadeOutDuration, new Color(1, 1, 1, 0));
        }

        void FocusArena(GameStateManager game) {
            // faz as câmeras focarem o Stratex, com zoom
            game.shipCamera1/*.SetTarget(game.stratex.transform, offset, zoomOutSpeed)*/.Zoom(zoomOutScale, zoomOutSpeed);
            game.shipCamera2/*.SetTarget(game.stratex.transform, offset, zoomOutSpeed)*/.Zoom(zoomOutScale, zoomOutSpeed);

            // oculta a HUD, aumentando o zoom da câmera
            //game.hudCamera1.Zoom(3, zoomOutSpeed);
            //game.hudCamera2.Zoom(3, zoomOutSpeed);
        }
    }
}