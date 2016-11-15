using System;
using UnityEngine;

public class ShipEngine : MonoBehaviour {

    [Header("Control")]
    public float trusterPower;
    public float maxSpeed;
    public float turningSpeed;

    [Header("Fuel")]
    public float startingFuel;
    public Transform fuelBar;

    Rigidbody2D rb;
    ShipInput input;
    float fuel;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ShipInput>();
        fuel = startingFuel;
        UpdateFuelBar();
    }

    void Update() {
        Thrust();
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
        if (fuel <= 0) return;
        if (!input.Thrusting) return;

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
