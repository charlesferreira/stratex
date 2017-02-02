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
    public Tremor screenShake;
    public AudioClip soundEffect;
    public float volume;
    public float pitch;
    public float pitchRange;

    public void PlaySound(Vector3 position) {
        var randomPitch = pitch + Random.Range(-pitchRange, pitchRange) / 2f;
        SoundFX.PlayClipAtPoint(soundEffect, position, volume, randomPitch);
    }
}
