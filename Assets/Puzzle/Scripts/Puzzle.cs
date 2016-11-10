using UnityEngine;

public class Puzzle : MonoBehaviour {

    public Transform alliedShip;

    PuzzleInput input;
    ShipInterface ship;

	void Start () {
        input = GetComponent<PuzzleInput>();
        ship = GetComponent<ShipInterface>();
	}
	
	void Update () {
	}
}
