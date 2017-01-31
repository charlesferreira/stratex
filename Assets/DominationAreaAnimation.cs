using UnityEngine;

public class DominationAreaAnimation : MonoBehaviour {

    [Range(0, 1)]
    public float speed = 1f;

    SpriteRenderer sr;
    Animator anim;

    float alpha = 0;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        anim.speed = speed;
        alpha = (alpha + speed * Time.deltaTime) % (alpha + speed);
        var color = sr.material.color;
        color.a = alpha;
        sr.material.color = color;
	}
}
