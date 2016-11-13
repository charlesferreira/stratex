using UnityEngine;

public class Weapon {

    WeaponInfo info;
    Transform ship;
    float cooldown;

    public Weapon(WeaponInfo info, Transform ship) {
        this.info = info;
        this.ship = ship;
    }

    public void Fire() {
        if (cooldown > 0) return;

        cooldown = info.cooldown;
        SpawnProjectile();
    }

    void SpawnProjectile() {
        var rotation = ship.rotation;
        var position = ship.position + ship.rotation * info.offset;
        GameObject.Instantiate(info.projectile, position, rotation);
    }
}
