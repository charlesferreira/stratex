using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float lifeTime;

    void Start() {
        Destroy(gameObject, lifeTime);
    }
	
	void Update () {
        var velocity = transform.right * speed * Time.deltaTime;
        transform.Translate(velocity);
	}
}
