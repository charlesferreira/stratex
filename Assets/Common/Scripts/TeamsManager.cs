using UnityEngine;

public class TeamsManager : MonoBehaviour {

    [SerializeField] Team team1;
    [SerializeField] Team team2;

    static TeamsManager instance;
    static public TeamsManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<TeamsManager>();
            return instance;
        }
    }

    public Team GetTeam(TeamFlags flag) {
        return (flag == team1.flag) ? team1 : team2;
    }

    public Team GetEnemyTeam(TeamFlags flag) {
        return (flag != team1.flag) ? team1 : team2;
    }
}
