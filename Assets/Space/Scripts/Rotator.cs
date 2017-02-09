using UnityEngine;

public class Rotator : MonoBehaviour {

    public bool randomStartAngle;
    public float speed;

    void Start() {
        if (randomStartAngle)
            transform.Rotate(0, 0, Random.Range(0, 360));
    }

    void Update() {
        transform.Rotate(0, 0, Time.deltaTime * speed);
    }
}