using UnityEngine;

public class ShipWeapons: MonoBehaviour {

    public WeaponInfo weaponInfo1;
    public WeaponInfo weaponInfo2;

    ShipInput input;

    Weapon weapon1;
    Weapon weapon2;

    void Start() {
        input = GetComponent<ShipInput>();

        weapon1 = new Weapon(weaponInfo1, transform);
        weapon2 = new Weapon(weaponInfo2, transform);
    }

    void Update () {
        if (input.Fire1) weapon1.Fire();
        if (input.Fire2) weapon2.Fire();
    }

    void OnDrawGizmos() {
        if (weaponInfo1) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position + weaponInfo1.offset, 0.05f);
        }

        if (weaponInfo2) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + weaponInfo2.offset, 0.05f);
        }
    }
}
