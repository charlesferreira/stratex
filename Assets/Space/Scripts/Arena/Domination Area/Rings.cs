using System.Collections;
using UnityEngine;

public class Rings : MonoBehaviour {

    [Range(0.001f, 10)]
    public float baseSpeed;
    [Range(0, 0.1f)]
    public float damping;
    [Range(0, 10)]
    public float speedUpFactor;
    [Range(0, 10)]
    public float overheatMultiplier;

    SpriteRenderer[] children;
    int currentRing;
    float elapsed;
    float targetSpeed;
    float currentSpeed;
    float speedMultiplier = 1;
    bool overheating;

    public float TimePerRing { get { return 1f / currentSpeed; } }

    void Start() {
        targetSpeed = currentSpeed = baseSpeed;
        children = GetComponentsInChildren<SpriteRenderer>();
        children[0].enabled = true;
    }

    void Update() {
        // corrige a velocidade atual
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, damping);

        // atualiza o tempo do anel
        elapsed += Time.deltaTime * Time.timeScale;
        if (elapsed > TimePerRing) {
            elapsed -= TimePerRing;
            NextRing();
        }

        // atualiza o alpha do anel
        var ring = children[currentRing];
        var color = ring.color;
        color.a = Mathf.PingPong(elapsed * 2f / TimePerRing, 1);
        ring.color = color;
    }

    void NextRing() {
        var previousRing = currentRing;
        currentRing += overheating ? Random.Range(0, children.Length) : 1;
        currentRing %= children.Length;

        children[previousRing].enabled = false;
        children[currentRing].enabled = true;
    }

    public void SpeedUp() {
        speedMultiplier++;
        UpdateTargetSpeed();
    }

    public void SlowDown() {
        overheating = false;
        speedMultiplier = Mathf.Max(0f, --speedMultiplier);
        UpdateTargetSpeed();
    }

    public void OverHeat() {
        overheating = true;
        speedMultiplier = overheatMultiplier;
        UpdateTargetSpeed();
    }

    public void ResetSpeed() {
        overheating = false;
        speedMultiplier = 1;
        UpdateTargetSpeed();
    }

    void UpdateTargetSpeed() {
        targetSpeed = baseSpeed * (speedMultiplier * speedUpFactor);
    }

    public IEnumerator Stop() {
        // pára o anel
        while (speedMultiplier > 0) {
            SlowDown();
            yield return new WaitForSeconds(0.25f);
        }

        // apaga o anel
        var ring = children[currentRing];
        var color = ring.color;
        while (color.a > 0.0001f) {
            color.a -= Time.fixedDeltaTime * color.a / 2;
            ring.color = color;
            yield return new WaitForFixedUpdate();
        }
    }
}
