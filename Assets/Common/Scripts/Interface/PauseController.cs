using UnityEngine;

public class PauseController : MonoBehaviour {

    public MenuInput pauseMenu;
    public MainMenu pauseScreen;
    public Joystick[] joysticks;

    public bool IsPaused { get; private set; }

    static PauseController instance;
    public static PauseController Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<PauseController>();
            return instance;
        }
    }

    void Update() {
        if (IsPaused) return;

        foreach (var joystick in joysticks) {
            if (Input.GetButtonDown(joystick.StartButton)) {
                pauseMenu.joysticks.Clear();
                pauseMenu.joysticks.Add(joystick);
                Pause();
                return;
            }
        }
    }

    public void Pause() {
        IsPaused = true;
        pauseMenu.ResetInputs();
        pauseScreen.enabled = true;
    }

    public void Resume() {
        IsPaused = false;
        pauseMenu.ResetInputs();
        pauseScreen.enabled = false;
    }
}
