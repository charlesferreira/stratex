using UnityEngine;
using System;

[Serializable]
public class Weapon {

    public WeaponInfo info;
    public Transform spawnPoint;
    
    int projectilesLayer;
    float cooldown;

    public void Init(int projectilesLayer) {
        this.projectilesLayer = projectilesLayer;
    }

    public void Fire() {
        if (cooldown > 0) return;

        cooldown = info.cooldown;
        SpawnProjectile();
    }

    void SpawnProjectile() {
        var projectile = GameObject.Instantiate(
            info.projectile, 
            spawnPoint.position, 
            spawnPoint.rotation) as GameObject;
        projectile.layer = projectilesLayer;
    }
}
