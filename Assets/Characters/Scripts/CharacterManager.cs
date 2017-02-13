using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour {

    public List<GameObject> cursors;
    public List<GameObject> cardsGameObjects;

    List<Vector3> cardsPositions = new List<Vector3>();
    List<CharacterCard> cards = new List<CharacterCard>();
    List<MenuInput> menuInput = new List<MenuInput>();
    List<Movement> movement = new List<Movement>();
    List<int> indexCursor = new List<int>();

    AudioSource switchAudio;
    AudioSource confirmAudio;
    AudioSource cancelAudio;

    public float movementDuration = .1f;

    void Start () {

        for (int i = 0; i < 2; i++)
        {
            cards.Add(cardsGameObjects[i].GetComponent<CharacterCard>());
            cardsPositions.Add(cardsGameObjects[i].transform.localPosition);
            movement.Add(cursors[i].GetComponent<Movement>());
            menuInput.Add(GetComponents<MenuInput>()[i]);
            cursors[i].transform.localPosition = cardsPositions[i];
            indexCursor.Add(i);
        }

        switchAudio = GetComponents<AudioSource>()[0];
        confirmAudio = GetComponents<AudioSource>()[1];
        cancelAudio = GetComponents<AudioSource>()[2];
    }
	
	void Update () {

        for (int index = 0; index < 2; index++)
            CheckInputs(index, 1 - index);
	}

    private void CheckInputs(int index, int indexOther)
    {
        if (menuInput[index].Up || menuInput[index].Down)
            SwitchIndex(index);

        if (menuInput[index].Confirm)
        {
            if (cards[indexCursor[index]].selected)
                return;

            cards[indexCursor[index]].SetSelected();
            confirmAudio.Play();

            if (indexCursor[index] == indexCursor[indexOther])
            {
                indexCursor[indexOther] = (indexCursor[indexOther] + 1) % 2;
                movement[indexOther].MoveTo(cardsPositions[indexCursor[indexOther]], movementDuration);
            }
        }
        else  if (menuInput[index].Cancel)
        {
            if (cards[indexCursor[index]].selected) {
                cards[indexCursor[index]].Deselect();
                cancelAudio.Play();
            }
            else if (!cards[indexCursor[indexOther]].selected)
                GetComponentInParent<TeamCard>().HideCharacters();
        }
    }

    private void SwitchIndex(int index)
    {
        if (cards[indexCursor[index]].selected)
            return;

        indexCursor[index] = (indexCursor[index] + 1) % 2;
        if (cards[indexCursor[index]].selected)
            indexCursor[index] = (indexCursor[index] + 1) % 2;

        movement[index].MoveTo(cardsPositions[indexCursor[index]], movementDuration);
        switchAudio.Play();
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
