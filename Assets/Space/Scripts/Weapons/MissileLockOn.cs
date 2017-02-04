using UnityEngine;

public class MissileLockOn : MonoBehaviour {

    public GameObject lockOn;

    int engagedMissiles = 0;

	void Start () {
        lockOn.SetActive(false);
	}

    public void OnEngage() {
        engagedMissiles++;
        lockOn.SetActive(true);
    }

    public void OnDisengage() {
        engagedMissiles--;
        if (engagedMissiles < 1)
            lockOn.SetActive(false);
    }
}
