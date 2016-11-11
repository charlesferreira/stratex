using UnityEngine;

public class Ship : MonoBehaviour {

    ShipInput input;
    PuzzleInterface puzzle;

	void Start () {
        input = GetComponent<ShipInput>();
        puzzle = GetComponent<PuzzleInterface>();
    }
	
	void Update () {
	    
	}
}
