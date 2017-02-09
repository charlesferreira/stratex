using UnityEngine;

public class ShipOnOffSwitch : MonoBehaviour {

    ShipInput input;

	void Awake () {
        input = GetComponent<ShipInput>();
	}

    public void TurnOn() {
        input.enabled = true;
    }

    public void TurnOff() {
        input.enabled = false;
    }
}
