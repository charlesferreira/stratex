using UnityEngine;

public class Pulse : MonoBehaviour {
    SpriteRenderer sprite;
    CircleCollider2D circle;
    float minForce;
    float maxForce;
    float matchSize;
    Vector3 baseScale;
    float baseRadius;

    public float DeltaForce { get { return maxForce - minForce; } }
    public float RelativeSize { get { return 1 + (matchSize - 3) / 4; } }
    public float Radius { get { return baseRadius * RelativeSize; } }
    public Vector3 Scale { get { return baseScale * RelativeSize; } }

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();

        baseScale = sprite.transform.localScale;
        baseRadius = circle.radius;
    }

    public void Init(float minForce, float maxForce) {
        this.minForce = minForce;
        this.maxForce = maxForce;
    }

    public void setMatchSize(float matchSize) {
        this.matchSize = matchSize;
    }

    public void OnAnimationStart() {
        sprite.enabled = true;
        circle.enabled = true;

        sprite.transform.localScale = Scale;
    }

    public void OnAnimationFinish() {
        sprite.enabled = false;
        circle.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        var otherRB = other.GetComponent<Rigidbody2D>();
        if (otherRB == null) return;

        var distance = other.transform.position - transform.position;
        var direction = distance.normalized;
        var intensity = minForce + DeltaForce * ((Radius + baseRadius) - distance.magnitude) / (Radius + baseRadius);

        var force = direction * intensity * RelativeSize;
        otherRB.AddForce(force, ForceMode2D.Impulse);
    }
}
