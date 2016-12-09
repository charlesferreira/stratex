using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour, IScoreObserver {

    public Text allyScore;
    public Text enemyScore;
    
    TeamFlags allyFlag;

    void Start() {
        allyFlag = GetComponent<TeamIdentity>().info.flag;
        TeamsManager.Instance.RegisterObserver(this);

        var score = TeamsManager.Instance.startingPoints.ToString();
        var allyInfo = TeamsManager.Instance.GetTeamInfo(allyFlag);
        var enemyInfo = TeamsManager.Instance.GetEnemyTeam(allyFlag);

        allyScore.text = score;
        allyScore.color = allyInfo.color;

        enemyScore.text = score;
        enemyScore.color = enemyInfo.color;
    }

    public void ScoreHasChanged(TeamFlags flag, int score) {
        var scoreText = flag == allyFlag ? allyScore : enemyScore;
        scoreText.text = score.ToString();
    }
}
