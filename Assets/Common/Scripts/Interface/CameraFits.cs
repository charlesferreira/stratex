using UnityEngine;
using System.Collections;

public class CameraFits : MonoBehaviour {

    Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        print(camera.pixelWidth);
        print(camera.pixelHeight);
    }
	
	// Update is called once per frame
	void Update () {
        camera.orthographicSize = 1280 * Screen.height / Screen.width * 0.5f;


    }
}
