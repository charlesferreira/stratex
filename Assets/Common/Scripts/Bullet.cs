using UnityEngine;

public class Bullet : MonoBehaviour {

    public ProjectileInfo info;

    protected Rigidbody2D rb;

    public Color Color { get; internal set; }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * info.speed;

        Invoke("DestroyProjectile", info.lifeTime);

        GetComponentInChildren<SpriteRenderer>().material.SetColor("_EmissionColor", Color);
        //GetComponentInChildren<SpriteRenderer>().color = Color;
    }

    void OnCollisionEnter2D(Collision2D other) {
        CancelInvoke("DestroyProjectile");
        DestroyProjectile();

        var screenShaker = other.gameObject.GetComponent<ScreenShaker>();
        if (screenShaker != null)
            screenShaker.Shake(info.onHitScreenShake);
    }

    protected virtual void DestroyProjectile() {
        info.PlayOnHitEffects(transform.position);
        Destroy(gameObject);
    }
}
