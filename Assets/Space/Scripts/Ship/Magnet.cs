using UnityEngine;
using System.Collections.Generic;

public class Magnet : MonoBehaviour {

    public float intensity;
    public float minRadius;

    Vector3 distance;
    float minRadiusSqr;
    List<Transform> attracting = new List<Transform>();
    List<Transform> removables = new List<Transform>();

    void Start() {
        minRadiusSqr = minRadius * minRadius;
    }

	void OnTriggerEnter2D(Collider2D other) {
        attracting.Add(other.transform);
    }

    void OnTriggerExit2D(Collider2D other) {
        attracting.Remove(other.transform);
    }

    void FixedUpdate() {
        foreach (var block in attracting) {
            if (block == null)
                removables.Add(block);
            else
                Attract(block);
        }
        ClearRemovables();
    }

    private void Attract(Transform block) {
        distance = transform.position - block.position;
        var attraction = intensity / Mathf.Max(distance.sqrMagnitude, minRadiusSqr);

        block.Translate(distance.normalized * attraction * Time.fixedDeltaTime);
    }

    private void ClearRemovables() {
        if (removables.Count == 0) return;

        foreach (var block in removables)
            attracting.Remove(block);
        removables.Clear();
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected() {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, minRadius);
    }
#endif
}
