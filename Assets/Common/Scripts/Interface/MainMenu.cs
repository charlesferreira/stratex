using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    List<Button> buttons = new List<Button>();
    MainMenuInput input;
    int currentIndexButton = 0;

    void Start()
    {
        input = GetComponent<MainMenuInput>();

        foreach (Transform child in transform)
        {
            buttons.Add(child.GetComponent<Button>());
        }
    }

    void Update()
    {
        if (!Pause.Instance.pause)
        {
            currentIndexButton = 0;
            return;
        }

        if (input.Up)
        {
            currentIndexButton = (buttons.Count + currentIndexButton - 1) % buttons.Count;
        }
        else if (input.Down)
        {
            currentIndexButton = (currentIndexButton + 1) % buttons.Count;
        }

        buttons[currentIndexButton].Select();

        if (input.ConfirmButton)
        {
            buttons[currentIndexButton].onClick.Invoke();
        }
        if (input.CancelButton)
        {
            Pause.Instance.DoPause();
        }
    }
}
