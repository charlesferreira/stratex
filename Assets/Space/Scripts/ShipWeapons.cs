using UnityEngine;

public class ShipWeapons: MonoBehaviour {

    ShipInput input;

    void Start() {
        input = GetComponent<ShipInput>();
    }

    void Update () {
        if (input.Fire1) {
            Debug.Log("Pew pew pew!");
        }

        if (input.Fire2) {
            Debug.Log("Fire in the hole!");
        }
    }
}
