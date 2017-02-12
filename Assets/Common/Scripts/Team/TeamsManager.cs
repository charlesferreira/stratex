using UnityEngine;
using System.Collections.Generic;

public class TeamsManager : MonoBehaviour {

    [Header("Teams")]
    [SerializeField] TeamInfo team1Info;
    [SerializeField] TeamInfo team2Info;

    Dictionary<TeamFlags, Team> teams;
    List<IScoreObserver> scoreObservers = new List<IScoreObserver>();

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            return instance;
        }
    }

    void Awake() {
        teams = new Dictionary<TeamFlags, Team>(2);
        teams.Add(team1Info.flag, new Team(team1Info));
        teams.Add(team2Info.flag, new Team(team2Info));
    }

    public TeamInfo GetTeamInfo(TeamFlags flag) {
        return teams[flag].info;
    }

    public TeamInfo GetEnemyTeam(TeamFlags flag) {
        var enemy = TeamFlags.Both & ~flag;
        return teams[enemy].info;
    }

    public void Score(TeamFlags flag) {
        var score = teams[flag].Score();
        NotifyScoreObservers(flag, score);
    }

    public int GetTeamScore(TeamFlags flag) {
        return teams[flag].Points;
    }

    void NotifyScoreObservers(TeamFlags flag, int score) {
        foreach (var observer in scoreObservers) {
            observer.ScoreHasChanged(flag, score);
        }
    }

    public void RegisterObserver(IScoreObserver observer) {
        scoreObservers.Add(observer);
    }
}
