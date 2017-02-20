using UnityEngine;

public class Scoreboard : MonoBehaviour, IScoreObserver {

    public TextMesh score;

    void Start () {
        TeamsManager.Instance.RegisterObserver(this);
	}

    public void ScoreHasChanged(TeamFlags flag, int score) {
        var score1 = TeamsManager.Instance.GetTeamScore(TeamFlags.Team1).ToString();
        var score2 = TeamsManager.Instance.GetTeamScore(TeamFlags.Team2).ToString();

        this.score.text = score1 + " x " + score2;
    }
    
    void OnDestroy() {
        if (TeamsManager.Instance != null)
            TeamsManager.Instance.RemoveObserver(this);
    }
}
