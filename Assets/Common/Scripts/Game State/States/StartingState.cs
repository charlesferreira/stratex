using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class StartingState : IGameState {

        [Range(1, 5)]
        public float zoomIn;
        [Range(0, 5)]
        public float zoomInDuration;
        [Range(0, 5)]
        public float showRolesDuration;

        public IEnumerator Play(GameStateManager game) {
            // oculta quaisquer mensagens
            CommonMessages.Instance.SetMessage(CommonMessages.MessageType.None);
            
            // desliga os controles das naves e dos puzzles, foca no Stratex
            game.TurnOffControls();
            game.FocusStratex(zoomIn);
            yield return new WaitForSeconds(zoomInDuration);

            // zoom out exibindo papéis dos jogadores
            ShowRoles(game);
            game.FocusPlayers();
            yield return new WaitForSeconds(showRolesDuration);

            // próximo estado
            game.ToPlayingState();
        }

        void ShowRoles(GameStateManager game) {
            Debug.Log("Mostrar papéis...");
        }
    }
}