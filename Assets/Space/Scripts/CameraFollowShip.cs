using UnityEngine;

public class CameraFollowShip : MonoBehaviour {

    [Header("References")]
    public Transform ship;

    [Header("Movement")]
    public float damping;
    public Vector2 offset;

    void FixedUpdate() {
        transform.position = Vector2.Lerp(transform.position, ship.position + (Vector3)offset, damping);
    }
}
