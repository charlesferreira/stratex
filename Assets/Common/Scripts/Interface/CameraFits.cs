using UnityEngine;
using System.Collections;

public class CameraFits : MonoBehaviour {

    Camera camera;
    int displayHeight = 720;
    int displayWidth = 1280;

    int lastScreenWidth;
    int lastScreenHeight;

    // Use this for initialization
    void Start() {
        camera = GetComponent<Camera>();

        SetCameraSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastScreenHeight != Screen.height || lastScreenWidth != Screen.width)
            SetCameraSize();
    }

    private void SetCameraSize()
    {
        if (Screen.height / Screen.width > displayHeight / 1280)
            camera.orthographicSize = 1280 * Screen.height / Screen.width * 0.5f;
        else
            camera.orthographicSize = displayHeight * 0.5f;

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }
}
