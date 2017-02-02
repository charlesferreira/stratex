using UnityEngine;

public class Bullet : MonoBehaviour {

    public ProjectileInfo info;
    public ParticleSystem hitExplosion;

    protected Rigidbody2D rb;

    void OnEnable() {
        Instantiate(info.soundEffect);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * info.speed;

        Destroy(gameObject, info.lifeTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        Explode(hitExplosion);
        Destroy(gameObject);
    }

    protected void Explode(ParticleSystem hitExplosion) {
        var explosion = Instantiate(hitExplosion, transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(explosion.gameObject, explosion.duration);
    }
}
