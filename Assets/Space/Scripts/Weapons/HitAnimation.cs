using UnityEngine;

public class HitAnimation : MonoBehaviour {

	void Start () {
        transform.Rotate(0, 0, Random.Range(0, 360));

        var anim = GetComponent<Animator>();
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

    public void Play(Vector3 position) {
        Instantiate(this, position, Quaternion.identity);
    }
}
