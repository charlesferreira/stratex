using UnityEngine;

public class CameraMan : MonoBehaviour {

    [Header("References")]
    public new Camera camera;
    public Transform target;

    [Header("Movement")]
    public float damping;
    public Vector2 offset;

    Transform originalTarget;
    Vector2 originalOffset;
    float originalSize;
    float zoomScale;
    float zoomSpeed;

    void Awake() {
        SetTarget(target, offset);
        Zoom(1, 1);
        originalSize = camera.orthographicSize;
    }

    void FixedUpdate() {
        if (target != null)
            FollowTarget();
        UpdateZoom();
    }

    void FollowTarget() {
        transform.position = Vector2.Lerp(transform.position, target.position + (Vector3)offset, damping);
    }

    void UpdateZoom() {
        var desiredSize = originalSize / zoomScale;
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, desiredSize, damping * zoomSpeed);
    }

    public CameraMan SetTarget(Transform target) {
        return SetTarget(target, Vector2.zero);
    }

    public CameraMan SetTarget(Transform target, Vector2 offset) {
        originalTarget = this.target;
        originalOffset = this.offset;
        this.target = target;
        this.offset = offset;
        return this;
    }

    public CameraMan ResetTarget() {
        target = originalTarget;
        offset = originalOffset;
        return this;
    }

    public CameraMan Zoom(float scale, float speed) {
        zoomScale = scale;
        zoomSpeed = speed;
        return this;
    }
}
