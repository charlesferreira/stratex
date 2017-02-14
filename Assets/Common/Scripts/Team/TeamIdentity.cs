using UnityEngine;

public class TeamIdentity : MonoBehaviour {

    public TeamFlags flag;

    public TeamInfo Info {
        get {
            return TeamsManager.Instance.GetTeamInfo(flag);
        }
    }

}
