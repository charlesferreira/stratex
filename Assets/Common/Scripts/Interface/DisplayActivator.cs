using UnityEngine;

public class DisplayActivator : MonoBehaviour {

    private static bool active = false;

    void Start() {
        if (!active)
            Activate();
    }

    void Activate() {
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        active = true;
    }
}
