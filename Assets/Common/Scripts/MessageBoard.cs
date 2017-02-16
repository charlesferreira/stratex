using System.Collections;
using UnityEngine;

public class MessageBoard : MonoBehaviour {

    public Transform content;
    public MessageInfo info;

    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

	void OnEnable() {
        StartCoroutine(Play());
    }

    IEnumerator Play() {
        // corrige a posição inicial
        var startingPosition = content.localPosition;
        startingPosition.x = info.startingX;
        content.localPosition = startingPosition;

        // abre o quadro
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(info.fadeIn);

        // slide in
        var speed = -info.startingX / info.slideIn;
        var slideIn = info.slideIn;
        while (slideIn > 0) {
            yield return new WaitForFixedUpdate();
            slideIn -= Time.fixedDeltaTime;
            content.Translate(Vector2.right * speed * Time.fixedDeltaTime, UnityEngine.Space.Self);
        }

        // wait
        yield return new WaitForSeconds(info.stay);

        // slide out
        var slideOut = info.slideOut;
        speed = -info.startingX / info.slideOut;
        while (slideOut > 0) {
            yield return new WaitForFixedUpdate();
            slideOut -= Time.fixedDeltaTime;
            content.Translate(Vector2.right * speed * Time.fixedDeltaTime, UnityEngine.Space.Self);
        }

        // fecha o quadro
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(info.fadeOut);

    }
}
