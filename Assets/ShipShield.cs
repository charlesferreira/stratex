using UnityEngine;

public class ShipShield : MonoBehaviour {

    [Header("Graphics")]
    public Transform shieldSprite;

    [Header("Logic")]
    public float startingTime;
    [Range(0f, 1f)]
    public float minAlpha;
    [Range(0f, 1f)]
    public float maxAlpha;
    [Range(0f, 10f)]
    public float alphaSpeed = 1f;

    float time;
    public bool IsActive { get { return time > 0f; } }

    SpriteRenderer sprite;
    CircleCollider2D shieldCollider;
    float deltaAlphaSign;

    ShipInput input;

    void Start() {
        sprite = shieldSprite.GetComponent<SpriteRenderer>();
        sprite.gameObject.layer = gameObject.layer;

        shieldCollider = sprite.GetComponent<CircleCollider2D>();

        input = GetComponent<ShipInput>();

        time = startingTime;
        ResetAlpha();
    }

    void Update() {
        time -= Time.deltaTime;
        time = Mathf.Max(0f, time);

        if (IsActive) {
            sprite.enabled = true;
            shieldCollider.enabled = true;

            var deltaAlpha = alphaSpeed * deltaAlphaSign * Time.deltaTime;
            SetAlpha(sprite.color.a + deltaAlpha);
        }
        else {
            sprite.enabled = false;
            shieldCollider.enabled = false;
        }

        if (input.Shield) AddTime(2);
    }

    public void AddTime(int time) {
        if (!IsActive)
            SetAlpha(maxAlpha);

        this.time += time;
    }

    void SetAlpha(float alpha) {
        if (alpha <= minAlpha) {
            deltaAlphaSign = 1;
        } else if (alpha >= maxAlpha) {
            deltaAlphaSign = -1;
        }

        var color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    void ResetAlpha() {
        SetAlpha(0);
        deltaAlphaSign = 1;
    }
}
