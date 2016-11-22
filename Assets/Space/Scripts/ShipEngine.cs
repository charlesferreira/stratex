using UnityEngine;

public class ShipEngine : MonoBehaviour {

    [Header("References")]
    public Transform fuelBar;
    public ParticleSystem primaryParticles;
    public ParticleSystem reserveParticles;

    [Header("Fuel")]
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

    public bool IsThrusting { get { return input.Thrusting; } }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ShipInput>();
        fuel = startingFuel;
        UpdateFuelBar();
    }

    void FixedUpdate() {
        if (IsThrusting)
            Accelerate();
    }

    void Update() {
        var primaryEmission = primaryParticles.emission;
        var reserveEmission = reserveParticles.emission;

        primaryEmission.enabled = false;
        reserveEmission.enabled = false;

        if (IsThrusting) {
            fuel = Mathf.Max(0f, fuel - Time.deltaTime);
            UpdateFuelBar();

            var emission = fuel > 0 ? primaryEmission : reserveEmission;
            emission.enabled = true;
        }

        Steer();
    }

    void LateUpdate() {
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Accelerate() {
        var power = fuel > 0 ? primaryThrusterPower : reserveThrusterPower;
        var force = transform.right * power * Time.fixedDeltaTime;
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
