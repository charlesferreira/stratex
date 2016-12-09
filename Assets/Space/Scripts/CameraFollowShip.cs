using UnityEngine;

public class CameraFollowShip : MonoBehaviour {

    [Header("References")]
    public Transform ship;

    [Header("Movement")]
    public float damping;

    void LateUpdate() {
        transform.position = Vector2.Lerp(transform.position, ship.position, damping);
    }
}
