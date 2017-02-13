using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

    Text display;

	void Start () {
        display = GetComponent<Text>();
        UpdateDisplay();
	}
	
	void Update () {
        UpdateDisplay();
    }

    void UpdateDisplay() {
        display.text = GameTimer.Instance.ToString();
    }
}
