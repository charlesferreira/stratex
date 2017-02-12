using UnityEngine;

public class ShipOnOffSwitch : MonoBehaviour {

    public Magnet magnet;

    ShipInput input;

	void Awake () {
        input = GetComponent<ShipInput>();
	}

    public void TurnOn() {
        input.enabled = magnet.enabled = true;
    }

    public void TurnOff() {
        input.enabled = magnet.enabled = false;
    }
}
