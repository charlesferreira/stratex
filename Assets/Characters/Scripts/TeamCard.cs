using UnityEngine;
using System.Collections;

public class TeamCard : MonoBehaviour {

    public TeamInfo info;
    public bool selected = false;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<SpriteRenderer>().sprite = info.teamCard;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
