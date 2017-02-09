using UnityEngine;

public class PuzzleOnOffSwitch : MonoBehaviour {
    
    Cursor cursor;

	void Awake() {
        cursor = GetComponentInChildren<Cursor>();
    }

    public void TurnOn() {
        cursor.Show();
    }

    public void TurnOff() {
        cursor.Hide();
    }
}
