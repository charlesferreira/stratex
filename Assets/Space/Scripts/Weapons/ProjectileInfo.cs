using UnityEngine;

[CreateAssetMenu(menuName = "Space/Projectile")]
public class ProjectileInfo : ScriptableObject {
    
    [Header("Basics")]
    public float speed;
    public float lifeTime;

    [Header("Steering")]
    public float steeringStrength;
    
    [Header("Hit Effects")]
    public Tremor onHitScreenShake;
    public SoundFX onHitSoundEffect;
    public HitAnimation onHitAnimation;
    public ParticleSystem onHitParticles;

    public void PlayOnHitEffects(Vector3 position) {
        if (onHitSoundEffect != null) onHitSoundEffect.Play(position);
        if (onHitAnimation != null) onHitAnimation.Play(position);
        if (onHitParticles != null) {
            var explosion = Instantiate(onHitParticles, position, Quaternion.identity) as ParticleSystem;
            Destroy(explosion.gameObject, explosion.duration);
        }
    }
}
