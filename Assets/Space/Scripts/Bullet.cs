using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public ParticleSystem explosionPrefab;

    protected Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as ParticleSystem;
        Destroy(explosion.gameObject, explosion.duration);
        Destroy(gameObject);
    }
}
