using UnityEngine;

public class Pause : MonoBehaviour {

    public Joystick[] joysticks;

    public bool pause = false;

    static Pause instance;
    public static Pause Instance
    {
        get {
            if (instance == null)
                instance = FindObjectOfType<Pause>();
            return instance;
        }
    }

    void Start () {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update () {

        if (pause) return;

        foreach (var joystick in joysticks)
        {
            if (Input.GetButtonDown(joystick.StartButton))
            {
                transform.GetChild(0).GetComponent<MainMenuInput>().joystick = joystick;
                DoPause();
            }
        }
	}

    public void DoPause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
