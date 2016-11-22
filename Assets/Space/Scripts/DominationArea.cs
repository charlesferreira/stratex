using System;
using UnityEngine;

public class DominationArea : MonoBehaviour {

    public MeshRenderer areaFlag;

    int numShipsInArea;

    void OnTriggerEnter2D(Collider2D other) {
        numShipsInArea++;

        var team = TeamsManager.Instance.GetTeam(other.tag);
        UpdateAreaColor(team);
    }

    void OnTriggerExit2D(Collider2D other) {
        numShipsInArea--;

        var team = TeamsManager.Instance.GetEnemyTeam(other.tag);
        UpdateAreaColor(team);
    }

    private void UpdateAreaColor(Team team) {
        var color = numShipsInArea == 1 ? team.color : Color.white;
        areaFlag.material.color = color;

    }
}
