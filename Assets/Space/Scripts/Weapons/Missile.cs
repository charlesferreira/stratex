﻿using UnityEngine;

public class Missile : Bullet {

    SpriteRenderer sprite;
    ParticleSystem trail;
    BoxCollider2D boxCollider;

    MissileLockOn target;

    protected override void Start() {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<ParticleSystem>();

        // pinta o rastro com a cor do time
        var main = trail.main;
        main.startColor = Color;

        // ativa o efeito de lock-on
        target.OnEngage();
    }

    void FixedUpdate() {
        // Acelera em direção ao alvo
        var direction = target.transform.position - transform.position;
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
        // desativa o efeito de lock-on
        target.OnDisengage();

        // desabilita colisor e sprite
        // todo: verificar por que o sprite às vezes é null
        if (sprite != null) sprite.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;
        if (trail != null) {
            // aguarda o sistema de partículas do rastro terminar para então destruir o objeto
            trail.Stop();
            var main = trail.main;
            Destroy(gameObject, main.duration);
        }

        // cria explosão
        info.PlayOnHitEffects(transform.position);
    }

    public void SetTarget(MissileLockOn target) {
        this.target = target;
    }
}
