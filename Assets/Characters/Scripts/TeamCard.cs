using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TeamCard : MonoBehaviour {

    public TeamInfo info;
    CharacterManager characterManager;
    [HideInInspector] public bool selected = false;
    GameObject cursor;

	void Start () {

        GetComponentInChildren<SpriteRenderer>().sprite = info.teamCard;
        characterManager = GetComponentInChildren<CharacterManager>();
        characterManager.SetCharactersSprites(info.character1, info.character2);
        characterManager.gameObject.SetActive(false);
    }
	
	void Update () {
	
	}

    internal void ShowCharacters(List<Joystick> joysticks, GameObject cursor)
    {
        this.cursor = cursor;
        cursor.SetActive(false);

        selected = true;

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        characterManager.gameObject.SetActive(true);

        characterManager.ShowCharacters(joysticks, cursor.GetComponent<TeamCursor>().colors);
    }

    internal void HideCharacters()
    {
        cursor.SetActive(true);

        selected = false;

        GetComponentInChildren<SpriteRenderer>().enabled = true;
        characterManager.gameObject.SetActive(false);
    }
}
