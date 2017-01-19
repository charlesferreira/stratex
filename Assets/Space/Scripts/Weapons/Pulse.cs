using System;
using UnityEngine;

public class Pulse : MonoBehaviour {
    SpriteRenderer sprite;
    CircleCollider2D circle;
    float minForce;
    float maxForce;

    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    public void OnAnimationStart() {
        sprite.enabled = true;
        circle.enabled = true;
    }

    internal void Init(float minForce, float maxForce) {
        this.minForce = minForce;
        this.maxForce = maxForce;
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
        var intensity = (maxForce - minForce) * distance.magnitude / circle.radius;
        var force = direction * intensity;
        otherRB.AddForce(force, ForceMode2D.Impulse);
    }
}
