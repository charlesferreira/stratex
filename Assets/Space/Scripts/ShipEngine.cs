using System;
using UnityEngine;

public class ShipEngine : MonoBehaviour {

    [Header("References")]
    public Transform fuelBar;
    public ParticleSystem extraParticles;

    [Header("Control")]
    public float trusterPower;
    public float maxSpeed;
    [Range(0, 1)]
    public float speedDamping = 0.997f;
    [Range(0, 1)]
    public float turningSpeed;

    [Header("Fuel")]
    public float startingFuel;

    Rigidbody2D rb;
    ShipInput input;
    float fuel;

    public bool IsThrusting { get { return fuel > 0 && input.Thrusting; } }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ShipInput>();
        fuel = startingFuel;
        UpdateFuelBar();
    }

    void FixedUpdate() {
        if (IsThrusting) {
            Thrust();
        }
    }

    void Update() {
        var emission = extraParticles.emission;
        if (IsThrusting) {
            fuel = Mathf.Max(0f, fuel - Time.deltaTime);
            UpdateFuelBar();
            emission.enabled = true;
        }
        else {
            emission.enabled = false;
        }

        rb.velocity = rb.velocity * speedDamping;
        Steer();
    }

    void LateUpdate() {
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Thrust() {
        var force = transform.right * trusterPower * Time.fixedDeltaTime;
        rb.AddForce(force);
    }

    void Steer() {
        var target = input.SteeringTarget;
        if (target == Vector2.zero)
            return;

        transform.right = Vector2.Lerp(transform.right, target, turningSpeed);
    }

    void UpdateFuelBar() {
        var scale = fuelBar.localScale;
        scale.x = fuel / startingFuel;
        fuelBar.transform.localScale = scale;
    }

    public void AddFuel(int fuel) {
        this.fuel += fuel;
        UpdateFuelBar();
    }
}
