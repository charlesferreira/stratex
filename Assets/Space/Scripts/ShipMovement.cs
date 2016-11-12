using UnityEngine;

public class ShipMovement : MonoBehaviour {
    
    public float trusterPower;
    public float maxSpeed;
    public float turningSpeed;

    Rigidbody2D rb;
    ShipInput input;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<ShipInput>();
    }

    void Update () {
        Thrust();
        Steer();
    }

    void LateUpdate () {
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Thrust () {
        if (!input.Thrusting) return;

        var force = transform.right * trusterPower * Time.deltaTime;
        rb.AddForce(force);
    }

    void Steer () {
        var steering = input.Steering * turningSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.back, steering);
    }
}
