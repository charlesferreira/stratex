using System;
using UnityEngine;

public class CameraFollowShips : MonoBehaviour {

    [Header("References")]
    public Camera spaceCamera;
    public Transform ship1;
    public Transform ship2;

    [Header("Settings")]
    [Range(0, 10)]
    public int weight;
    [Range(0, 1)]
    public float damping;

    int TotalWeight { get { return weight + 2; } }

    Vector2 target;

    void Update() {
        target = (ship1.position + ship2.position) / TotalWeight;
        transform.position = Vector2.Lerp(transform.position, target, damping);
    }
}
