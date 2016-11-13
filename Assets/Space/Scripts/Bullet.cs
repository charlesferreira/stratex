using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float lifeTime;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
