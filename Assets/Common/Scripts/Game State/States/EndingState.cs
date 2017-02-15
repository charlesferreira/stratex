using System.Collections;
using UnityEngine;

namespace GameStates {

    [System.Serializable]
    public class EndingState : IGameState {

        [Header("References")]
        public Animator result1;
        public Animator result2;

        [Header("Animations")]
        public RuntimeAnimatorController victory;
        public RuntimeAnimatorController defeat;
        public RuntimeAnimatorController draw;

        public IEnumerator Play(GameStateManager game) {
            ShowResultMenu();

            var score1 = TeamsManager.Instance.GetTeamScore(TeamFlags.Team1);
            var score2 = TeamsManager.Instance.GetTeamScore(TeamFlags.Team2);

            var animation1 = score1 > score2 ? victory : (score1 == score2 ? draw : defeat);
            var animation2 = score1 < score2 ? victory : (score1 == score2 ? draw : defeat);

            result1.runtimeAnimatorController = animation1;
            result2.runtimeAnimatorController = animation2;

            result1.gameObject.SetActive(true);
            result2.gameObject.SetActive(true);

            yield return null;
        }

        void ShowResultMenu() {
            var messages = CommonMessages.Instance;
            messages.SetMessage(CommonMessages.MessageType.Result);
            messages.Show();
        }
    }
}