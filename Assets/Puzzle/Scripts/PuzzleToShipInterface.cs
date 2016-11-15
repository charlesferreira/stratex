using UnityEngine;

public class PuzzleToShipInterface : MonoBehaviour {

    public Transform alliedShip;

    ShipEngine engine;
    Weapon weapon1;
    Weapon weapon2;
    ShipShield shield;

    void Start() {
        engine = alliedShip.GetComponent<ShipEngine>();
        shield = alliedShip.GetComponent<ShipShield>();

        var weapons = alliedShip.GetComponent<ShipWeapons>();
        weapon1 = weapons.weapon1;
        weapon2 = weapons.weapon2;
    }

    public void NotifyMatch(Match match)
    {
        switch (match.color) {
            case BlockColor.Blue:
                engine.AddFuel(match.size);
                break;

            case BlockColor.Purple:
                shield.AddTime(match.size - 1);
                break;

            case BlockColor.Green:
                weapon1.AddAmmo(match.size);
                break;

            case BlockColor.Yellow:
                weapon2.AddAmmo(match.size - 2);
                break;
        }
    }
}
