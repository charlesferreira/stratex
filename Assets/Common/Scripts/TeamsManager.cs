using UnityEngine;

public class TeamsManager : MonoBehaviour {

    public Team team1;
    public Team team2;

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            return instance;
        }
    }

    public Team GetTeam(string tag) {
        return (tag == team1.tag) ? team1 : team2;
    }

    public Team GetEnemyTeam(string tag) {
        return (tag != team1.tag) ? team1 : team2;
    }
}
