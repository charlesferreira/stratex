using UnityEngine;

[ExecuteInEditMode]
public class ArenaBoundaries : MonoBehaviour {

    PolygonCollider2D pc;
    LineRenderer lr;

    void Start() {
        pc = GetComponent<PolygonCollider2D>();
        lr = GetComponent<LineRenderer>();

        UpdateRenderer();
    }

    void UpdateRenderer() {
        if (pc == null) return;

        var path = pc.GetPath(0);
        var positions = new Vector3[path.Length + 1];
        for (int i = 0; i < path.Length; i++) {
            positions[i] = path[i];
        }
        positions[positions.Length - 1] = path[0];
        
        lr.SetVertexCount(positions.Length);
        lr.SetPositions(positions);
    }

    void OnDrawGizmos() {
        UpdateRenderer();
    }
}
