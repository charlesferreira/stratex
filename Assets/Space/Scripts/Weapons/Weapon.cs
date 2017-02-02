using UnityEngine;

[System.Serializable]
public class Weapon {

    public WeaponInfo info;
    public Transform spawnPoint;
    //public Transform ammoText;
    
    int ammo;
    float cooldown;
    int projectilesLayer;
    //TextMesh text;
    Rigidbody2D rb;
    ScreenShaker screenShaker;

    public void Init(int projectilesLayer, Rigidbody2D rb, ScreenShaker screenShaker) {
        this.projectilesLayer = projectilesLayer;
        this.rb = rb;
        this.screenShaker = screenShaker;
        ammo = info.startingAmmo;
        //text = ammoText.GetComponent<TextMesh>();
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
        //text.text = ammo.ToString();
    }

    public void AddAmmo(int ammo) {
        this.ammo += ammo;
        UpdateAmmoText();
    }

    GameObject SpawnProjectile() {
        var projectile = Object.Instantiate(
            info.projectile,
            spawnPoint.position,
            spawnPoint.rotation) as GameObject;
        projectile.layer = projectilesLayer;

        // sound effect: fire
        info.PlaySound(spawnPoint.position);

        // juice: less accuracy
        var angle = Random.Range(-info.spreadAngle, info.spreadAngle) / 2f;
        projectile.transform.Rotate(0, 0, angle);

        // juice: recoil
        ApplyRecoil(projectile.transform.right);

        // juice: screen shake
        screenShaker.Shake(info.screenShake);

        return projectile;
    }
}
