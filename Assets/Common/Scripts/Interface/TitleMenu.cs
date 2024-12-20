﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

    List<Button> buttons = new List<Button>();
    MenuInput input;
    int currentIndexButton = 0;

    AudioSource switchAudio;

    void Awake()
    {
        input = GetComponent<MenuInput>();

        foreach (Transform child in transform)
        {
            buttons.Add(child.GetComponent<Button>());
        }

        switchAudio = GetComponents<AudioSource>()[0];
    }

    private void OnEnable()
    {
        currentIndexButton = 0;
    }
    void Update()
    {
        if (input.Up)
        {
            currentIndexButton = (buttons.Count + currentIndexButton - 1) % buttons.Count;
            switchAudio.Play();
        }
        else if (input.Down)
        {
            currentIndexButton = (currentIndexButton + 1) % buttons.Count;
            switchAudio.Play();
        }

        buttons[1].Select();
        buttons[currentIndexButton].Select();

        if (input.Confirm)
        {
            buttons[currentIndexButton].onClick.Invoke();
            SoundPlayer.Instance.confirmAudio.Play();
        }
    }
}
