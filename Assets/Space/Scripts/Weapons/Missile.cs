using UnityEngine;

public class Missile : Bullet {

    [Header("Missile specifics")]
    public float steeringStrength;
    public ParticleSystem expiredExplosion;

    Transform target;
    bool hit = false;

    void FixedUpdate() {
        // Acelera em direção ao alvo
        var direction = target.position - transform.position;
        var steeringForce = direction.normalized * steeringStrength * Time.fixedDeltaTime;
        rb.AddForce(steeringForce);
    }

    void LateUpdate() {
        // Mantém a velocidade limite
        rb.velocity = rb.velocity.normalized * speed;

        // Aponta na direção do movimento
        transform.right = rb.velocity;
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
        hit = true;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    void OnDestroy() {
        if (!hit) {
            Explode(expiredExplosion);
        }
    }
}
