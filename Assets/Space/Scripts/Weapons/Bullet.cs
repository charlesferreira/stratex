using System;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public ParticleSystem hitExplosion;

    protected Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
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
