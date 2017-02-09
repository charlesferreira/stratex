using UnityEngine;

public class Rotor : MonoBehaviour {

    [Range(0, 100)]
    public float baseSpeed;
    [Range(0, 0.1f)]
    public float damping;
    [Range(0, 10)]
    public float speedUpFactor;
    [Range(0, 10)]
    public float overheatMultiplier;

    float targetSpeed;
    float currentSpeed;
    float speedMultiplier;

    public float SpeedMultiplier { get { return speedMultiplier; } }

    void Start() {
        targetSpeed = currentSpeed = baseSpeed;
    }

    void Update() {
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

    public void OverHeat() {
        speedMultiplier = overheatMultiplier;
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
