using UnityEngine;

[CreateAssetMenu(menuName = "Space/Weapon")]
public class WeaponInfo : ScriptableObject {

    public Transform projectile;
    public Vector3 offset;
    public float cooldown;
}
