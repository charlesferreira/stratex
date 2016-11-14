using UnityEngine;

public class ShipWeapons: MonoBehaviour {

    public SingleUnityLayer projectilesLayer;
    public Transform enemyShip;

    [Header("Weapons")]
    public Weapon weapon1;
    public Weapon weapon2;

    ShipInput input;

    void Start() {
        input = GetComponent<ShipInput>();
        InitializeWeapons();
    }

    void InitializeWeapons() {
        weapon1.Init(projectilesLayer);
        weapon2.Init(projectilesLayer);
    }

    void Update () {
        if (input.Fire1) weapon1.Fire();
        if (input.Fire2) LaunchMissile();
    }

    void LaunchMissile() {
        var missile = weapon2.Fire();
        if (missile == null) return;
        missile.GetComponent<Missile>().SetTarget(enemyShip);
    }
}
