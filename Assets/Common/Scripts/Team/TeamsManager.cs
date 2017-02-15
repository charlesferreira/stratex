using UnityEngine;
using System.Collections.Generic;

public class TeamsManager : MonoBehaviour {

    Dictionary<TeamFlags, Team> teams = new Dictionary<TeamFlags, Team>(2);
    Dictionary<TeamFlags, Joystick> pilots = new Dictionary<TeamFlags, Joystick>(2);
    Dictionary<TeamFlags, Joystick> engineers = new Dictionary<TeamFlags, Joystick>(2);
    List<IScoreObserver> scoreObservers = new List<IScoreObserver>();

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            if (instance == null) {
                var obj = new GameObject();
                instance = obj.AddComponent<TeamsManager>();
            }
            return instance;
        }
    }

    public void Init(TeamInfo team1, Joystick pilot1, Joystick engineer1, TeamInfo team2, Joystick pilot2, Joystick engineer2) {
        // cria os times
        teams.Clear();
        teams.Add(TeamFlags.Team1, new Team(team1));
        teams.Add(TeamFlags.Team2, new Team(team2));

        // controles das naves
        pilots.Clear();
        pilots.Add(TeamFlags.Team1, pilot1);
        pilots.Add(TeamFlags.Team2, pilot2);

        // controles dos puzzles
        engineers.Clear();
        engineers.Add(TeamFlags.Team1, engineer1);
        engineers.Add(TeamFlags.Team2, engineer2);
    }

    public TeamInfo GetTeamInfo(TeamFlags flag) {
        return teams[flag].info;
    }

    public TeamInfo GetEnemyTeam(TeamFlags flag) {
        var enemy = TeamFlags.Both & ~flag;
        return teams[enemy].info;
    }

    public Joystick GetPilot(TeamFlags flag) {
        return pilots[flag];
    }

    public Joystick GetEngineer(TeamFlags flag) {
        return engineers[flag];
    }

    public void Score(TeamFlags flag) {
        var score = teams[flag].Score();
        NotifyScoreObservers(flag, score);
    }

    public int GetTeamScore(TeamFlags flag) {
        return teams[flag].Points;
    }

    // Score observers

    void NotifyScoreObservers(TeamFlags flag, int score) {
        foreach (var observer in scoreObservers) {
            observer.ScoreHasChanged(flag, score);
        }
    }

    public void RegisterObserver(IScoreObserver observer) {
        scoreObservers.Add(observer);
    }
}
