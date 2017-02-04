using UnityEngine;

public class Missile : Bullet {

    SpriteRenderer sprite;
    ParticleSystem trail;
    BoxCollider2D boxCollider;

    Transform target;

    new void Start() {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<ParticleSystem>();
        trail.startColor = Color;
    }

    void FixedUpdate() {
        // Acelera em direção ao alvo
        var direction = target.position - transform.position;
        var steeringForce = direction.normalized * info.steeringStrength * Time.fixedDeltaTime;
        rb.AddForce(steeringForce);
    }

    void LateUpdate() {
        // Mantém a velocidade limite
        rb.velocity = rb.velocity.normalized * info.speed;

        // Aponta na direção do movimento
        transform.right = rb.velocity;
    }

    protected override void PlayOnHitEffects() {
        base.PlayOnHitEffects();

        // aguarda o sistema de partículas do rastro terminar
        boxCollider.enabled = false;
        sprite.enabled = false;
        trail.Stop();
    }

    protected override void DestroyProjectile() {
        Destroy(gameObject, trail.duration);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
