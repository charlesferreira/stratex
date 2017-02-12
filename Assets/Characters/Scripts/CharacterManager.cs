using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour {

    public List<GameObject> cursors;
    public List<GameObject> cards;

    List<MenuInput> menuInput = new List<MenuInput>();
    List<Movement> movement = new List<Movement>();
    List<int> indexCursor = new List<int>();

    public float movementDuration = .1f;

    void Start () {

        for (int i = 0; i < 2; i++)
        {
            movement.Add(cursors[i].GetComponent<Movement>());
            menuInput.Add(GetComponents<MenuInput>()[i]);
            cursors[i].transform.position = cards[i].transform.position;
            indexCursor.Add(i);
        }
    }
	
	void Update () {

        for (int index = 0; index < 2; index++)
            CheckInpputs(index);
	}

    private void CheckInpputs(int index)
    {
        if (menuInput[index].Up || menuInput[index].Down)
        {
            indexCursor[index] = indexCursor[index] == 0 ? 1 : 0;
            movement[index].MoveTo(cards[indexCursor[index]].transform.localPosition, movementDuration);
        }
        if (menuInput[index].Cancel)
        {
            GetComponentInParent<TeamCard>().HideCharacters();
        }
    }

    internal void ShowCharacters(List<Joystick> joysticks, List<Color> colors)
    {
        for (int i = 0; i < 2; i++)
        {
            GetComponents<MenuInput>()[i].joysticks[0] = joysticks[i];
            cursors[i].GetComponentInChildren<SpriteRenderer>().color = colors[i];
        }
    }

    public void SetCharactersSprites(Sprite character1, Sprite character2)
    {
        GetComponentsInChildren<SpriteRenderer>()[0].sprite = character1;
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = character2;
    }
}
