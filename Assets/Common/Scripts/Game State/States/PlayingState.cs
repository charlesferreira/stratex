using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class PlayingState : IGameState {

        [Range(0, 1)]
        public float maxAlpha;
        [Range(0, 2)]
        public float goFadeIn;
        [Range(0, 2)]
        public float goStay;
        [Range(0, 2)]
        public float goFadeOut;

        public void OnStateEnter(GameStateManager game) {
            TurnOnControls(game);
            ShowStartMessage();
            game.StartCoroutine(HideStartMessage());
            game.StartCoroutine(TurnOnControls(game));
        }

        void ShowStartMessage() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Go);
            messages.Show(goFadeIn, new Color(1, 1, 1, maxAlpha));
        }

        IEnumerator HideStartMessage() {
            yield return new WaitForSeconds(goFadeIn + goStay);
            CommonMessages.Instance.Hide(goFadeOut, new Color(1, 1, 1, 0));
        }

        IEnumerator TurnOnControls(GameStateManager game) {
            yield return new WaitForSeconds(goFadeIn + goStay / 3);
            // habilita os controles das naves e dos puzzles
            game.ship1.TurnOn();
            game.ship2.TurnOn();
            game.puzzle1.TurnOn();
            game.puzzle2.TurnOn();
        }

        public void Update(GameStateManager game) {
        }
    }
}