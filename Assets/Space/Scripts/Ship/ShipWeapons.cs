using UnityEngine;

public class ShipWeapons: MonoBehaviour {

    public SingleUnityLayer projectilesLayer;
    public Transform enemyShip;
    public ScreenShaker screenShaker;

    [Header("Weapons")]
    public Weapon weapon1;
    public Weapon weapon2;

    ShipInput input;
    Rigidbody2D rb;

    void Start() {
        input = GetComponent<ShipInput>();
        rb = GetComponent<Rigidbody2D>();
        InitializeWeapons();
    }

    void InitializeWeapons() {
        var color = GetComponent<TeamIdentity>().info.color;
        weapon1.Init(color, projectilesLayer, rb, screenShaker);
        weapon2.Init(color, projectilesLayer, rb, screenShaker);
    }

    void FixedUpdate () {
        weapon1.Update();
        weapon2.Update();

        if (input.Fire1) weapon1.Fire();
        if (input.Fire2) LaunchMissile();
    }

    void LaunchMissile() {
        var missile = weapon2.Fire();
        if (missile == null) return;
        missile.GetComponent<Missile>().SetTarget(enemyShip);
    }
}
