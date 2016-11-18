using UnityEngine;

[ExecuteInEditMode]
public class ArenaBoundaries : MonoBehaviour {

    new PolygonCollider2D collider;

    void Start() {
        collider = GetComponent<PolygonCollider2D>();
        UpdatePaths();
    }

    private void UpdatePaths() {
        collider.pathCount = transform.childCount;
        for (int i = 0; i < transform.childCount; i++) {
            SetColliderPath(i, transform.GetChild(i));
        }
    }

    void SetColliderPath(int index, Transform t) {
        Vector2[] points = new Vector2[t.childCount];
        for (int i = 0; i < t.childCount; i++) {
            points[i] = t.GetChild(i).position;
        }

        collider.SetPath(index, points);
    }

    void OnDrawGizmos() {
        if (collider == null) return;

        UpdatePaths();

        Gizmos.color = Color.red;
        Vector2 thisNode, nextNode;

        for (int i = 0; i < collider.pathCount; i++) {
            var path = collider.GetPath(i);
            for (int j = 0; j < path.Length; j++) {
                thisNode = path[j];
                nextNode = path[(j + 1) % path.Length];
                Gizmos.DrawLine(thisNode, nextNode);
            }
        }
    }
}
