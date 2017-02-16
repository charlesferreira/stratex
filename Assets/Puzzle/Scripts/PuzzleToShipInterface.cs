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

        var power = match.info.Power(match.size);
        switch (match.info.color) {
            case BlockColor.Eletric:
                pulse.Fire(match.size);
                break;

            case BlockColor.Missile:
                weapon2.AddAmmo(power);
                break;

            case BlockColor.Fuel:
                engine.AddFuel(power);
                break;

            case BlockColor.Shield:
                shield.AddTime(power);
                break;

            case BlockColor.Laser:
                weapon1.AddAmmo(power);
                break;
        }
    }
}
