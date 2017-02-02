using UnityEngine;

public class Planet : MonoBehaviour {

    public float speed;

    void Start() {
        transform.Rotate(0, 0, Random.Range(0, 360));
    }

	void Update () {
        transform.Rotate(0, 0, Time.deltaTime * speed);
	}
}
