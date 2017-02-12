using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour, IScoreObserver {

    public Text score1;
    public Text score2;

    void Start () {
        TeamsManager.Instance.RegisterObserver(this);
	}

    public void ScoreHasChanged(TeamFlags flag, int score) {
        score1.text = TeamsManager.Instance.GetTeamScore(TeamFlags.Team1).ToString();
        score2.text = TeamsManager.Instance.GetTeamScore(TeamFlags.Team2).ToString();
    }
}
