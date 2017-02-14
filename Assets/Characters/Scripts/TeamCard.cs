using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TeamCard : MonoBehaviour {

    public TeamInfo info;
    public float flashDuration = 0.4f;
    CharacterManager characterManager;
    [HideInInspector] public bool selected = false;

    GameObject cursor;
    List<Joystick> joysticks;

    AudioSource confirmAudio;
    AudioSource cancelAudio;

    bool leaving;

    void Start () {

        GetComponentInChildren<SpriteRenderer>().sprite = info.teamCard;
        characterManager = GetComponentInChildren<CharacterManager>();
        characterManager.SetCharactersSprites(info.pilot, info.engineer);
        characterManager.gameObject.SetActive(false);

        confirmAudio = GetComponents<AudioSource>()[0];
        cancelAudio = GetComponents<AudioSource>()[1];
    }

    void Update () {

        if (leaving)
        {
            selected = false;
            leaving = false;
        }
	}

    internal void ShowCharacters(List<Joystick> joysticks, GameObject cursor)
    {
        this.cursor = cursor;
        this.joysticks = joysticks;

        cursor.SetActive(false);

        selected = true;

        GetComponentInChildren<FlashSprite>().StartFlash();
        Invoke("Show", flashDuration);

        leaving = false;
        confirmAudio.Play();
    }

    private void Show()
    {
        GetComponentInChildren<FlashSprite>().StopFlash();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        characterManager.gameObject.SetActive(true);
        characterManager.ShowCharacters(joysticks, cursor.GetComponent<TeamCursor>().colors);
    }

    internal void HideCharacters()
    {
        cursor.SetActive(true);

        leaving = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<MeshRenderer>().enabled = true;
        characterManager.gameObject.SetActive(false);
        cancelAudio.Play();
    }
}
