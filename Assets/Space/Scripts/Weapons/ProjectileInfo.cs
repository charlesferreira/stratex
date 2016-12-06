using UnityEngine;

[CreateAssetMenu(menuName = "Space/Projectile")]
public class ProjectileInfo : ScriptableObject {
    
    [Header("Basics")]
    public float speed;
    public float lifeTime;

    [Header("Steering")]
    public float steeringStrength;
}
