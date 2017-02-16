using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class PausedState : IGameState {

        public IEnumerator Play(GameStateManager game) {
            Pause(game);
            
            while (PauseController.Instance.IsPaused)
                yield return null;

            Resume(game);
        }

        void Pause(GameStateManager game) {
            // pára a música
            SoundPlayer.Pause();

            // pára o tempo e exibe a tela de pause
            Time.timeScale = 0;
            GameTimer.Instance.Pause();
            ShowPauseScreen();

            // desliga os controles
            game.TurnOffControls();
        }

        void Resume(GameStateManager game) {
            // oculta tela de pause
            CommonMessages.Instance.Hide();
            Time.timeScale = 1;

            // retoma a música
            SoundPlayer.UnPause();

            // volta pro estado "jogando"
            game.ToPlayingState();
        }

        void ShowPauseScreen() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Pause);
            messages.Show();
        }
    }
}