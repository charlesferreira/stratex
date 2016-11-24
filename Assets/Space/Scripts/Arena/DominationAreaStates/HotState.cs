using System;
using UnityEngine;

namespace DominationAreaStates {

    public class HotState : AbstractState {
        
        private float timeToScoreNextPoint;

        public HotState(DominationArea dominationArea) : base(dominationArea) { }

        public override void OnStateEnter() {
            base.OnStateEnter();

            Score();
            timeToScoreNextPoint = dominationArea.pointDuration;
        }

        public override void ShipHasEntered(TeamInfo team) {
            ToOverheatedState();
        }

        public override void ShipHasLeft(TeamInfo team) {
            ToColdState();
        }

        public override void Update() {
            base.Update();

            timeToScoreNextPoint -= Time.deltaTime;
            if (timeToScoreNextPoint <= 0f) {
                timeToScoreNextPoint += dominationArea.pointDuration;
                Score();
            }

            var glow = Mathf.PingPong(elapsedTime, dominationArea.HotGlowTime) / dominationArea.HotGlowTime;
            var glowColor = (DominatingTeam.color + dominationArea.hotGlowColor) / 2f;
            dominationArea.Color = Color.Lerp(DominatingTeam.color, glowColor, glow);
        }

        private void Score() {
            TeamsManager.Instance.Score(dominationArea.CurrentTeam);
        }
    }
}
