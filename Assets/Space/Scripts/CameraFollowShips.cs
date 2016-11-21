using System;
using UnityEngine;

public class CameraFollowShips : MonoBehaviour {

    [Header("References")]
    public Camera spaceCamera;
    public Transform ship1;
    public Transform ship2;

    [Header("Movement")]
    [Range(0, 10)]
    public float weight;
    [Range(0.01f, 1)]
    public float speedDamping;

    [Header("Zoom")]
    [Range(0, 100)]
    public float maxDistanceBetweenShips;
    [Range(1, 3)]
    public float maxZoomFactor;
    [Range(0.2f, 1)]
    public float minZoomFactor;
    [Range(0.01f, 1)]
    public float zoomDamping;

    float TotalWeight { get { return weight + 2f; } }

    Vector2 target;
    float baseCameraSize;
    float zoom = 1f;

    void Start() {
        baseCameraSize = spaceCamera.orthographicSize;
    }

    void LateUpdate() {
        target = (ship1.position + ship2.position) / TotalWeight;
        transform.position = Vector2.Lerp(transform.position, target, speedDamping);

        ApplyZoom();
    }

    void ApplyZoom() {
        var distance = (ship1.position - ship2.position).magnitude;
        var relativeDist = Mathf.Clamp01(distance / maxDistanceBetweenShips);
        var deltaZoom = (maxZoomFactor - minZoomFactor) * relativeDist;
        var targetZoom = minZoomFactor + deltaZoom;
        zoom = Mathf.Lerp(zoom, targetZoom, zoomDamping);
        spaceCamera.orthographicSize = baseCameraSize * zoom;
    }
}
