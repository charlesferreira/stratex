using UnityEngine;

public class ShipHUD : MonoBehaviour {

    public GameObject hud;

    ShipInput input;

	// Use this for initialization
	void Start () {
        input = GetComponent<ShipInput>();
        hud.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        hud.SetActive(input.ShowHUD);
    }
}
