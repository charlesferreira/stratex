using UnityEngine;

public class Bullet : MonoBehaviour {

    public ProjectileInfo info;

    protected Rigidbody2D rb;

    public Color Color { get; internal set; }

    protected void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * info.speed;

        Invoke("DestroyProjectile", info.lifeTime);

        GetComponentInChildren<SpriteRenderer>().material.SetColor("_EmissionColor", Color);
        //GetComponentInChildren<SpriteRenderer>().color = Color;
    }

    void OnCollisionEnter2D(Collision2D other) {
        ShakeScreen(other.gameObject);
        PlayOnHitEffects();
        DestroyProjectile();
    }

    private void ShakeScreen(GameObject other) {
        var screenShaker = other.GetComponent<ScreenShaker>();
        if (screenShaker != null)
            screenShaker.Shake(info.onHitScreenShake);
    }

    protected virtual void PlayOnHitEffects() {
        CancelInvoke("DestroyProjectile");
        info.PlayOnHitEffects(transform.position);
    }

    protected virtual void DestroyProjectile() {
        Destroy(gameObject);
    }
}
