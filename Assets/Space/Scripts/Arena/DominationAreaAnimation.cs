using UnityEngine;

public class DominationAreaAnimation : MonoBehaviour {

    [Range(0, 1)]
    public float speed;
    public float frames;

    SpriteRenderer sr;
    Animator anim;

    float alpha;
    float blinkTime;
    float elapsed;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // todo: rever esse método inteiro, escrevi morrendo de sono desmaiando sobre o teclado
    void Update() {
        anim.speed = speed;
        
        blinkTime = anim.GetCurrentAnimatorStateInfo(0).length / frames;

        elapsed += Time.deltaTime;
        elapsed %= blinkTime;

        alpha = Mathf.PingPong(elapsed * 2, 1);

        var color = sr.material.color;
        color.a = alpha;
        sr.material.color = color;
    }
}
