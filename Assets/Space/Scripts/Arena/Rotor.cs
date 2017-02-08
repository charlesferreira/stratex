using UnityEngine;

public class Rotor : MonoBehaviour {
    
    public float baseSpeed;
    [Range(0, 1)]
    public float damping;
    public float speedUpFactor;

    float targetSpeed;
    float currentSpeed;
    public float speedMultiplier;

    void Start() {
        targetSpeed = currentSpeed = baseSpeed;
    }

    void Update() {
        UpdateTargetSpeed();
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, damping);
        transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime * Time.timeScale);
    }

    public void SpeedUp() {
        speedMultiplier++;
        UpdateTargetSpeed();
    }

    public void SlowDown() {
        speedMultiplier = Mathf.Max(0f, --speedMultiplier);
        UpdateTargetSpeed();
    }

    public void ResetSpeed() {
        speedMultiplier = 0;
        UpdateTargetSpeed();
    }

    void UpdateTargetSpeed() {
        targetSpeed = baseSpeed * (1 + speedMultiplier * speedUpFactor);
    }
}
