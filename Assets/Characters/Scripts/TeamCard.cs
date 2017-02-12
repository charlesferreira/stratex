using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TeamCard : MonoBehaviour {

    public TeamInfo info;
    CharacterManager characterManager;
    [HideInInspector] public bool selected = false;

	void Start () {

        GetComponentInChildren<SpriteRenderer>().sprite = info.teamCard;
        characterManager = GetComponentInChildren<CharacterManager>();
        characterManager.SetCharactersSprites(info.character1, info.character2);
        characterManager.gameObject.SetActive(false);
    }
	
	void Update () {
	
	}

    internal void ShowCharacters(List<Joystick> joysticks, List<Color> colors)
    {
        characterManager.GetComponents<MenuInput>()[0].joysticks[0] = joysticks[0];
        characterManager.GetComponents<MenuInput>()[1].joysticks[0] = joysticks[1];

        characterManager.cursors[0].GetComponentInChildren<SpriteRenderer>().color = colors[0];
        characterManager.cursors[1].GetComponentInChildren<SpriteRenderer>().color = colors[1];

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        characterManager.gameObject.SetActive(true);
    }
}
