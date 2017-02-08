﻿using UnityEngine;

public class ShipEngine : MonoBehaviour {

    [Header("References")]
    public Transform fuelHUD;
    public ParticleSystem neutralParticles;
    public ParticleSystem primaryParticles;
    public ParticleSystem reserveParticles;

    [Header("Capacity")]
    public float maxFuel;
    [Range(0, 1)]
    public float startingFuel;

    [Header("Control")]
    public float primaryThrusterPower;
    public float reserveThrusterPower;
    public float maxSpeed;
    [Range(0, 1)]
    public float turningSpeed;

    [Header("Legacy Controls")]
    public float legacyTurningSpeed = 180f;

    Rigidbody2D rb;
    ShipInput input;
    float fuel;
    Vector3 hudScale = Vector3.one;

    public bool IsThrusting { get { return input.Thrusting; } }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ShipInput>();
        fuel = maxFuel * startingFuel;
        UpdateHuD();
    }

    void FixedUpdate() {
        if (Pause.Instance.pause) return;

        if (IsThrusting)
            Accelerate();
    }

    void Update() {

        if (Pause.Instance.pause) return;

        var primaryEmission = primaryParticles.emission;
        var reserveEmission = reserveParticles.emission;
        var neutralEmission = neutralParticles.emission;

        primaryEmission.enabled = false;
        reserveEmission.enabled = false;

        if (IsThrusting) {
            fuel = Mathf.Max(0f, fuel - Time.deltaTime);
            UpdateHuD();

            var emission = fuel > 0 ? primaryEmission : reserveEmission;
            emission.enabled = true;
            neutralEmission.enabled = false;
        } else {
            neutralEmission.enabled = true;
        }

        Steer();
    }

    void LateUpdate() {
        if (Pause.Instance.pause) return;

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Accelerate() {
        var power = fuel > 0 ? primaryThrusterPower : reserveThrusterPower;
        var force = transform.right * power * Time.fixedDeltaTime * Time.timeScale;
        rb.AddForce(force);
    }

    void Steer() {
        if (input.usingLegacyControls) {
            SteerLegacy();
            return;
        }

        var target = input.SteeringTarget;
        if (target == Vector2.zero)
            return;

        transform.right = Vector2.Lerp(transform.right, target, turningSpeed);
    }

    void SteerLegacy() {
        var steering = input.LegacySteering * legacyTurningSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.back, steering);
        return;
    }


    void UpdateHuD() {
        hudScale.x = fuel / maxFuel;
        fuelHUD.localScale = hudScale;
    }

    public void AddFuel(int fuel) {
        this.fuel = Mathf.Min(maxFuel, this.fuel + fuel);
        UpdateHuD();
    }
}
