using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public List<Button> buttons = new List<Button>();

    MenuInput input;
    int currentIndexButton = 0;

    void Awake() {
        input = GetComponent<MenuInput>();
    }

    private void OnEnable() {
        currentIndexButton = 0;
    }

    void Update() {
        if (input.Up) {
            currentIndexButton = (buttons.Count + currentIndexButton - 1) % buttons.Count;
        }
        else if (input.Down) {
            currentIndexButton = (currentIndexButton + 1) % buttons.Count;
        }

        buttons[1].Select();
        buttons[currentIndexButton].Select();

        if (input.Confirm) {
            buttons[currentIndexButton].onClick.Invoke();
        }

        if (input.Cancel || input.Start) {
            PauseController.Instance.Resume();
        }
    }
}
