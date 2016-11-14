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

    public GameObject Fire() {
        if (cooldown > 0) return null;

        cooldown = info.cooldown;
        return SpawnProjectile();
    }

    GameObject SpawnProjectile() {
        var projectile = GameObject.Instantiate(
            info.projectile, 
            spawnPoint.position, 
            spawnPoint.rotation) as GameObject;
        projectile.layer = projectilesLayer;
        return projectile;
    }
}
