using System;
using UnityEngine;

public class ShipShield : MonoBehaviour {

    [Header("References")]
    public GameObject shield;

    [Header("Settings")]
    public float startingTime;
    [Range(0f, 1f)]
    public float minAlpha;
    [Range(0f, 1f)]
    public float maxAlpha;
    [Range(0f, 1f)]
    public float alphaSpeed = 1f;

    SpriteRenderer sprite;
    float time;

    void Start() {
        sprite = shield.GetComponent<SpriteRenderer>();
        shield.layer = gameObject.layer;
        time = startingTime;
    }

    void Update() {
        time -= Time.deltaTime;
        time = Mathf.Max(0f, time);
        
        shield.SetActive(time > 0);
        UpdateAlpha();
    }

    void UpdateAlpha() {
        if (time <= 0) return;

        // calcula o novo alpha do escudo para fazer "piscar"
        var theta = time * alphaSpeed * Mathf.Rad2Deg;
        var delta = (Mathf.Sin(theta) + 1f) / 2;
        var alpha = minAlpha + (maxAlpha - minAlpha) * delta;

        // aplica o alpha no escudo
        var color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    public void AddTime(int time) {
        this.time += time;
    }
}
