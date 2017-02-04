using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Weapon {

    public WeaponInfo info;
    public Text ammoHUD;
    public List<Transform> spawnPoints = new List<Transform>();
    
    int ammo;
    float cooldown;
    int projectilesLayer;
    int spawnPointIndex = 0;
    Color color;
    Rigidbody2D rb;
    ScreenShaker screenShaker;

    public void Init(Color color, int projectilesLayer, Rigidbody2D rb, ScreenShaker screenShaker) {
        this.color = color;
        this.projectilesLayer = projectilesLayer;
        this.rb = rb;
        this.screenShaker = screenShaker;
        ammo = info.startingAmmo;
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

    private void ApplyRecoil(Vector2 direction) {
        var force = -direction * info.recoil;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void UpdateAmmoText() {
        ammoHUD.text = ammo.ToString();
    }

    public void AddAmmo(int ammo) {
        this.ammo += ammo;
        UpdateAmmoText();
    }

    GameObject SpawnProjectile() {
        var spawnPoint = GetNextSpawnPoint();
        var projectile = Object.Instantiate(
            info.projectile,
            spawnPoint.position,
            spawnPoint.rotation) as GameObject;
        projectile.layer = projectilesLayer;

        // pinta o projétil com a cor do time
        projectile.GetComponent<Bullet>().Color = color;

        // juice: less accuracy
        var angle = Random.Range(-info.spreadAngle, info.spreadAngle) / 2f;
        projectile.transform.Rotate(0, 0, angle);

        // juice: recoil
        ApplyRecoil(projectile.transform.right);

        // juice: screen shake
        screenShaker.Shake(info.screenShakeOnFire);

        // sound effect
        info.soundEffectOnFire.Play(spawnPoint.position);

        return projectile;
    }

    private Transform GetNextSpawnPoint() {
        spawnPointIndex++;
        spawnPointIndex %= spawnPoints.Count;
        return spawnPoints[spawnPointIndex];
    }
}
