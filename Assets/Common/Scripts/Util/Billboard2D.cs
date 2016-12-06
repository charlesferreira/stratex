using UnityEngine;

[ExecuteInEditMode]
public class Billboard2D : MonoBehaviour {

    void LateUpdate() {
        transform.right = Vector2.right;
    }
}
