using UnityEngine;

public class ShipWeapons: MonoBehaviour {

    public ShipInput input;

    void Update () {
        if (input.Fire1) {
            Debug.Log("Pew pew pew!");
        }

        if (input.Fire2) {
            Debug.Log("Fire in the hole!");
        }
    }
}
