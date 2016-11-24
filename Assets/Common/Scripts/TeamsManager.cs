using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TeamsManager : MonoBehaviour {

    [Range(0, 100)]
    public int startingPoints;

    [Header("Teams")]
    [SerializeField] TeamInfo team1Info;
    [SerializeField] TeamInfo team2Info;

    [Header("GUI")]
    public Text team1ScoreText;
    public Text team2ScoreText;

    Dictionary<TeamFlags, Team> teams;

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            return instance;
        }
    }

    void Start() {
        teams = new Dictionary<TeamFlags, Team>(2);
        teams.Add(team1Info.flag, new Team(team1Info, startingPoints));
        teams.Add(team2Info.flag, new Team(team2Info, startingPoints));
        UpdateScoreText();
    }

    public TeamInfo GetTeamInfo(TeamFlags flag) {
        return teams[flag].info;
    }

    public TeamInfo GetEnemyTeam(TeamFlags flag) {
        var enemy = TeamFlags.Both & ~flag;
        return teams[enemy].info;
    }

    public void Score(TeamFlags flag) {
        teams[flag].Score();
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        team1ScoreText.text = teams[TeamFlags.Team1].Points.ToString();
        team2ScoreText.text = teams[TeamFlags.Team2].Points.ToString();
    }
}
