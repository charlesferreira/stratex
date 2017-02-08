using UnityEngine;

public class ObjectsActivator : MonoBehaviour {

    public GameObject[] objectsToEnable;

	void OnEnable () {
        for (int i = 0; i < objectsToEnable.Length; i++) {
            objectsToEnable[i].SetActive(true);
        }
	}
}
