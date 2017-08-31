using UnityEngine;

public class CameraFits : MonoBehaviour {

    Camera cameraRef;
    int displayHeight = 720;
    int displayWidth = 1280;

    int lastScreenWidth;
    int lastScreenHeight;

    // Use this for initialization
    void Start() {
        cameraRef = GetComponent<Camera>();

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
        if (Screen.height / Screen.width > displayHeight / displayWidth)
            cameraRef.orthographicSize = displayWidth * Screen.height / Screen.width * 0.5f;
        else
            cameraRef.orthographicSize = displayHeight * 0.5f;

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }
}
