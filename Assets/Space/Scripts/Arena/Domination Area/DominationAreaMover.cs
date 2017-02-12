using UnityEngine;

public class DominationAreaMover : MonoBehaviour {
    
    public Transform ship1;
    public Transform ship2;
    public Transform[] stopPoints;

    Transform target;

    void Start() {
        target = transform;
    }

    void FixedUpdate () {
        var translation = target.position - transform.position;
        transform.Translate(translation * Time.fixedDeltaTime, UnityEngine.Space.World);
	}

    public void SelectNextPoint() {
        var maxSqrDist = 0f;
        float sqrDist1, sqrDist2, sqrDist;
        for (int i = 0; i < stopPoints.Length; i++) {
            var position = stopPoints[i].position;
            sqrDist1 = (ship1.position - position).sqrMagnitude;
            sqrDist2 = (ship2.position - position).sqrMagnitude;
            sqrDist = Mathf.Min(sqrDist1, sqrDist2) / Mathf.Max(sqrDist1, sqrDist2);
            if (sqrDist > maxSqrDist) {
                maxSqrDist = sqrDist;
                target = stopPoints[i];
            }
        }
    }
}
