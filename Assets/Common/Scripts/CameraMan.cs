using UnityEngine;

public class CameraMan : MonoBehaviour {

    [Header("References")]
    public Camera cameraRef;
    public Transform target;

    [Header("Movement")]
    public float damping;
    public float speed;
    public Vector2 offset;

    Transform originalTarget;
    Vector2 originalOffset;
    float originalSize;
    float originalSpeed;
    float zoomScale;
    float zoomSpeed;

    void Awake() {
        SetTarget(target, offset, speed);
        Zoom(1, 1);
        originalSize = cameraRef.orthographicSize;
    }

    void FixedUpdate() {
        if (target != null)
            FollowTarget();
        UpdateZoom();
    }

    void FollowTarget() {
        transform.position = Vector2.Lerp(transform.position, target.position + (Vector3)offset, damping * speed);
    }

    void UpdateZoom() {
        var desiredSize = originalSize / zoomScale;
        cameraRef.orthographicSize = Mathf.Lerp(cameraRef.orthographicSize, desiredSize, damping * zoomSpeed);
    }

    public CameraMan SetTarget(Transform target) {
        return SetTarget(target, Vector2.zero, 1);
    }

    public CameraMan SetTarget(Transform target, float speed) {
        return SetTarget(target, Vector2.zero, speed);
    }

    public CameraMan SetTarget(Transform target, Vector2 offset, float speed) {
        originalTarget = this.target;
        originalOffset = this.offset;
        originalSpeed = this.speed;
        this.target = target;
        this.offset = offset;
        this.speed = speed;
        return this;
    }

    public CameraMan ResetTarget() {
        target = originalTarget;
        offset = originalOffset;
        speed = originalSpeed;
        return this;
    }

    public CameraMan Zoom(float scale, float speed) {
        zoomScale = scale;
        zoomSpeed = speed;
        return this;
    }

    public CameraMan Zoom(float scale) {
        return Zoom(scale, Mathf.Infinity);
    }
}
