using UnityEngine;
using System.Collections.Generic;

public class TeamsManager : MonoBehaviour {

    [Header("Team 1")]
    public SpriteRenderer pilotFrame1;
    public SpriteRenderer engineerFrame1;

    [Header("Team 1")]
    public SpriteRenderer pilotFrame2;
    public SpriteRenderer engineerFrame2;

    Dictionary<TeamFlags, Team> teams = new Dictionary<TeamFlags, Team>(2);
    Dictionary<TeamFlags, Joystick> pilots = new Dictionary<TeamFlags, Joystick>(2);
    Dictionary<TeamFlags, Joystick> engineers = new Dictionary<TeamFlags, Joystick>(2);
    List<IScoreObserver> scoreObservers = new List<IScoreObserver>();

    TeamInfo teamInfo1;
    TeamInfo teamInfo2;

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            return instance;
        }
    }

    public void Init(TeamInfo team1, Joystick pilot1, Joystick engineer1, TeamInfo team2, Joystick pilot2, Joystick engineer2) {
        teamInfo1 = team1;
        teamInfo2 = team2;

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

    void Start() {
        // pilot instructions
        pilotFrame1.sprite = teamInfo1.instructionsPilot;
        pilotFrame2.sprite = teamInfo2.instructionsPilot;

        // engineer instructions
        engineerFrame1.sprite = teamInfo1.instructionsEngineer;
        engineerFrame2.sprite = teamInfo2.instructionsEngineer;
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
