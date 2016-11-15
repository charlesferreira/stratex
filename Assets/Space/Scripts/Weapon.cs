using UnityEngine;
using System;

[Serializable]
public class Weapon {

    public WeaponInfo info;
    public Transform spawnPoint;
    public Transform ammoText;

    TextMesh text;
    int ammo;
    float cooldown;
    int projectilesLayer;

    public void Init(int projectilesLayer) {
        this.projectilesLayer = projectilesLayer;
        ammo = info.startingAmmo;
        text = ammoText.GetComponent<TextMesh>();
        UpdateAmmoText();
    }

    public void Update() {
        cooldown -= Time.deltaTime;
    }

    public GameObject Fire() {
        if (cooldown > 0) return null;
        if (ammo <= 0) return null;

        ammo--;
        UpdateAmmoText();
        cooldown = info.cooldown;
        return SpawnProjectile();
    }

    void UpdateAmmoText() {
        text.text = ammo.ToString();
    }

    public void AddAmmo(int ammo) {
        this.ammo += ammo;
        UpdateAmmoText();
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
