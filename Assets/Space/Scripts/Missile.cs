using UnityEngine;

public class Missile : Bullet {

    [Header("Missile specifics")]
    public float steeringStrength;

    Transform target;

    void Update() {
        // Acelera em direção ao alvo
        var direction = target.position - transform.position;
        var steeringForce = direction.normalized * steeringStrength * Time.deltaTime;
        rb.AddForce(steeringForce);

        // Mantém a velocidade limite
        rb.velocity = rb.velocity.normalized * speed;
        
        // Aponta na direção do movimento
        var angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D other) {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(explosion.gameObject, explosion.duration);
        Destroy(gameObject);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
