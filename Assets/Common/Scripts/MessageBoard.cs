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
        content.Translate(Vector2.right * info.startingX, UnityEngine.Space.Self);

        anim.SetTrigger("Open");
        yield return new WaitForSeconds(info.fadeIn);

        var speed = -info.startingX / info.slideIn;
        while (content.localPosition.x < 0) {
            content.Translate(Vector2.right * speed * Time.deltaTime);
            yield return null;
        }

        var position = content.localPosition;
        position.x = 0;
        content.localPosition = position;

        yield return new WaitForSeconds(info.stay);

        speed = -info.startingX / info.slideOut;
        while (content.localPosition.x < -info.startingX) {
            content.Translate(Vector2.right * speed * Time.deltaTime);
            yield return null;
        }

        anim.SetTrigger("Close");
        yield return new WaitForSeconds(info.fadeOut);

    }
}
