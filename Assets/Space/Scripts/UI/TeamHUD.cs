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
        allyFlag = GetComponent<TeamIdentity>().info.flag;
        TeamsManager.Instance.RegisterObserver(this);

        var score = TeamsManager.Instance.startingPoints.ToString();
        var allyInfo = TeamsManager.Instance.GetTeamInfo(allyFlag);
        var enemyInfo = TeamsManager.Instance.GetEnemyTeam(allyFlag);

        allyScore.text = score;
        allyScore.color = allyInfo.color;
        neonBorder.color = allyInfo.color;
        neonAllyDetail.color = allyInfo.color;

        enemyScore.text = score;
        enemyScore.color = enemyInfo.color;
        neonEnemyDetail.color = enemyInfo.color;
    }

    public void ScoreHasChanged(TeamFlags flag, int score) {
        var scoreText = flag == allyFlag ? allyScore : enemyScore;
        scoreText.text = score.ToString();
    }
}
