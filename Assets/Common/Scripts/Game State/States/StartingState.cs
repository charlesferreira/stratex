using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class StartingState : IGameState {

        [Header("Stratex")]
        [Range(1, 5)]
        public float zoomIn;
        [Range(0, 5)]
        public float zoomInDuration;

        [Header("Roles")]
        [Range(0, 5)]
        public float showRolesDuration;
        public GameObject pilot1;
        public GameObject pilot2;
        public GameObject engineer1;
        public GameObject engineer2;

        public IEnumerator Play(GameStateManager game) {
            // zera o timer
            GameTimer.Instance.Reset();

            // oculta quaisquer mensagens
            HideRoles();
            CommonMessages.Instance.SetMessage(CommonMessages.MessageType.InstructionsStratex);
            
            // desliga os controles das naves e dos puzzles, foca no Stratex
            game.TurnOffControls();
            game.Focus(game.stratexZoomFocus, zoomIn);
            yield return new WaitForSeconds(zoomInDuration);

            // zoom out exibindo papéis dos jogadores
            CommonMessages.Instance.SetMessage(CommonMessages.MessageType.None);
            GridManager.Instance.StartGrids();
            ShowRoles();
            game.FocusPlayers();
            yield return new WaitForSeconds(showRolesDuration);

            // próximo estado
            HideRoles();
            game.ToPlayingState();
        }

        void ShowRoles() {
            pilot1.SetActive(true);
            pilot2.SetActive(true);
            engineer1.SetActive(true);
            engineer2.SetActive(true);
        }

        void HideRoles() {
            pilot1.SetActive(false);
            pilot2.SetActive(false);
            engineer1.SetActive(false);
            engineer2.SetActive(false);
        }
    }
}