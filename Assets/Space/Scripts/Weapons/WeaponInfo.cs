using UnityEngine;

[CreateAssetMenu(menuName = "Space/Weapon")]
public class WeaponInfo : ScriptableObject {

    public GameObject projectile;
    public Vector3 offset;
    public float cooldown;
    public int startingAmmo;
    public float spreadAngle;
    public float recoil;
    public Tremor screenShake;
}
