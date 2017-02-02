using UnityEngine;
using Space.Arena.BlocksDistribution;

public class PuzzleToShipInterface : MonoBehaviour {

    public Transform alliedShip;

    ShipEngine engine;
    Weapon weapon1;
    Weapon weapon2;
    ShipPulse pulse;
    ShipShield shield;

    void Start() {
        engine = alliedShip.GetComponent<ShipEngine>();
        shield = alliedShip.GetComponent<ShipShield>();

        var weapons = alliedShip.GetComponent<ShipWeapons>();
        weapon1 = weapons.weapon1;
        weapon2 = weapons.weapon2;
        pulse = alliedShip.GetComponent<ShipPulse>();
    }

    public void NotifyMatch(Match match)
    {
        for (int i = 0; i < match.size; i++) {
            if (BlockFactory.Instance != null)
                BlockFactory.Instance.CreateBlock(match.info);
        }

        switch (match.info.color) {
            case BlockColor.Blue:
                pulse.Fire(match.size);
                break;

            case BlockColor.Orange:
                weapon2.AddAmmo(match.size - 2);
                break;

            case BlockColor.Green:
                engine.AddFuel(match.size);
                break;

            case BlockColor.Yellow:
                shield.AddTime(match.size - 1);
                break;

            case BlockColor.Red:
                weapon1.AddAmmo(match.size);
                break;
        }
    }
}
