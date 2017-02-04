using UnityEngine;

public class Missile : Bullet {

    SpriteRenderer sprite;
    ParticleSystem trail;
    BoxCollider2D boxCollider;

    Transform target;

    protected override void Start() {
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

    protected override void DestroyProjectile() {
        // desabilita colisor e sprite
        // todo: verificar por que o sprite às vezes é null
        sprite.enabled = false;
        boxCollider.enabled = false;

        // aguarda o sistema de partículas do rastro terminar para então destruir o objeto
        trail.Stop();
        Destroy(gameObject, trail.duration);

        // cria explosão
        info.PlayOnHitEffects(transform.position);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
