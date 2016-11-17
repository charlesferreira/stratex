using System;
using UnityEngine;

public class ShipEngine : MonoBehaviour {

    [Header("References")]
    public Transform fuelBar;
    public ParticleSystem extraParticles;

    [Header("Control")]
    public float trusterPower;
    public float maxSpeed;
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

    void Update() {
        var emission = extraParticles.emission;
        if (IsThrusting) {
            Thrust();
            emission.enabled = true;
        }
        else {
            emission.enabled = false;
        }

        rb.velocity = rb.velocity * 0.999f;
        Steer();
    }

    void UpdateFuelBar() {
        var scale = fuelBar.localScale;
        scale.x = fuel / startingFuel;
        fuelBar.transform.localScale = scale;
    }

    void LateUpdate() {
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Thrust() {
        var force = transform.right * trusterPower * Time.deltaTime;
        rb.AddForce(force);

        fuel = Mathf.Max(0f, fuel - Time.deltaTime);
        UpdateFuelBar();
    }

    void Steer() {
        var steering = input.Steering * turningSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.back, steering);
    }

    public void AddFuel(int fuel) {
        this.fuel += fuel;
        UpdateFuelBar();
    }
}
