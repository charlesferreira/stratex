using System.Collections;
using UnityEngine;

public class TeamCursorAlternate : MonoBehaviour {

    public SpriteRenderer cursor;
    public float interval;

    float direction = 1;
    
	void Start () {
        cursor.transform.Translate(Vector3.back * 0.5f);
        StartCoroutine(Alternate());
	}

    IEnumerator Alternate() {
        while (true) {
            yield return new WaitForSeconds(interval);
            cursor.transform.Translate(Vector3.forward * direction);
            direction *= -1;
        }
    }
}
