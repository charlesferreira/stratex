using UnityEngine;
using System.Collections;

public class Planeta : MonoBehaviour {

    public float speed;
    public float angle;

	void Update () {
        var direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad));
        transform.Translate(direction * speed * Time.deltaTime * Time.timeScale);
	}
}
