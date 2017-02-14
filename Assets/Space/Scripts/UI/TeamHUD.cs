using UnityEngine;
using UnityEngine.UI;

public class TeamHUD : MonoBehaviour, IScoreObserver {

    public Text allyScore;
    public Text enemyScore;
    public RawImage neonBorder;
    public RawImage neonAllyDetail;
    public RawImage neonEnemyDetail;

    TeamFlags allyFlag;

    void Start() {
        allyFlag = GetComponent<TeamIdentity>().flag;
        TeamsManager.Instance.RegisterObserver(this);

        var score = "0";
        var allyInfo = TeamsManager.Instance.GetTeamInfo(allyFlag);
        var enemyInfo = TeamsManager.Instance.GetEnemyTeam(allyFlag);

        allyScore.text = score;
        allyScore.color = allyInfo.scoreColor;
        neonBorder.color = allyInfo.neonColor;
        neonAllyDetail.color = allyInfo.neonColor;

        enemyScore.text = score;
        enemyScore.color = enemyInfo.scoreColor;
        neonEnemyDetail.color = enemyInfo.neonColor;
    }

    public void ScoreHasChanged(TeamFlags flag, int score) {
        var scoreText = flag == allyFlag ? allyScore : enemyScore;
        scoreText.text = score.ToString();
    }
}
