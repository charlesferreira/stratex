using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TeamCard : MonoBehaviour {

    public TeamInfo info;
    CharacterManager characterManager;
    [HideInInspector] public bool selected = false;
    GameObject cursor;

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
        leaving = false;
        this.cursor = cursor;
        cursor.SetActive(false);

        selected = true;

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        characterManager.gameObject.SetActive(true);

        characterManager.ShowCharacters(joysticks, cursor.GetComponent<TeamCursor>().colors);
        confirmAudio.Play();
    }

    internal void HideCharacters()
    {
        cursor.SetActive(true);

        leaving = true;

        GetComponentInChildren<SpriteRenderer>().enabled = true;
        characterManager.gameObject.SetActive(false);
        cancelAudio.Play();
    }
}
