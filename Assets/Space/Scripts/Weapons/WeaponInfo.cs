using UnityEngine;

[CreateAssetMenu(menuName = "Space/Weapon")]
public class WeaponInfo : ScriptableObject {

    [Header("Spawn")]
    public GameObject projectile;
    public Vector3 offset;

    [Header("Fire settings")]
    public float cooldown;
    public int startingAmmo;
    public float spreadAngle;
    public float recoil;

    [Header("Effects")]
    public Tremor screenShakeOnFire;
    public SoundFX soundEffectOnFire;
}
