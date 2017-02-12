using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour {

    public List<GameObject> cursors;
    public List<GameObject> cards;

    MenuInput menuInput1;
    MenuInput menuInput2;

    Movement movement1;
    Movement movement2;

    public float movementDuration = .1f;

    int indexCursor1 = 0;
    int indexCursor2 = 1;

    void Start () {

        movement1 = cursors[0].GetComponent<Movement>();
        movement2 = cursors[1].GetComponent<Movement>();

        menuInput1 = GetComponents<MenuInput>()[0];
        menuInput2 = GetComponents<MenuInput>()[1];

        cursors[0].transform.position = cards[0].transform.position;
        cursors[1].transform.position = cards[1].transform.position;
    }
	
	void Update () {

        CheckInpputs(menuInput1, movement1, ref indexCursor1);
	}

    private void CheckInpputs(MenuInput menuInput, Movement movement, ref int indexCursor)
    {
        if (menuInput.Up || menuInput.Down)
        {
            indexCursor = indexCursor == 0 ? 1 : 0;
            movement.MoveTo(cards[indexCursor].transform.localPosition, movementDuration);
        }
    }

    public void SetCharactersSprites(Sprite character1, Sprite character2)
    {
        GetComponentsInChildren<SpriteRenderer>()[0].sprite = character1;
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = character2;
    }
}
