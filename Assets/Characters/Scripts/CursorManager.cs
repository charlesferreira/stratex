using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour {

    public List<TeamCard> cards;

    public List<GameObject> cursors = new List<GameObject>();
    List<MenuInput> menuInput = new List<MenuInput>();
    List<Movement> movementCursor = new List<Movement>();
    List<TeamCursor> teamCursor = new List<TeamCursor>();
    List<int> indexCursor = new List<int>();
    AudioSource switchAudio;

    public float movementDuration = .1f;

    void Start () {

        for (int i = 0; i < cursors.Count; i++)
        {
            menuInput.Add(GetComponents<MenuInput>()[i]);
            movementCursor.Add(cursors[i].GetComponent<Movement>());
            teamCursor.Add(cursors[i].GetComponent<TeamCursor>());
            indexCursor.Add(i * (cards.Count - 1));
            cursors[i].transform.position = cards[indexCursor[i]].transform.position;
        }

        switchAudio = GetComponent<AudioSource>();
    }

    void Update ()
    {
        for (int index = 0; index < 2; index++)
        {
            CheckInputs(index, 1 - index);
        }
    }

    private void CheckInputs(int index, int indexOther)
    {
        if (cards[indexCursor[index]].selected)
            return;

        if (menuInput[index].Right)
            CheckIndexCursor(index, 1);

        else if (menuInput[index].Left)
            CheckIndexCursor(index, -1);

        if (menuInput[index].Confirm)
        {
            cards[indexCursor[index]].ShowCharacters(menuInput[index].joysticks, cursors[index]);

            if (indexCursor[index] == indexCursor[indexOther])
            {
                if (indexCursor[indexOther] == cards.Count - 1)
                    indexCursor[indexOther] = (cards.Count + indexCursor[indexOther] - 1) % cards.Count;
                else
                    indexCursor[indexOther] = (indexCursor[indexOther] + 1) % cards.Count;
                movementCursor[indexOther].MoveTo(cards[indexCursor[indexOther]].transform.localPosition, movementDuration);
            }
        }
        else if (menuInput[index].Cancel)
        {
            SoundPlayer.Instance.cancelAudio.Play();
            SceneManager.LoadScene(0);
        }
    }

    private void CheckIndexCursor(int index, int step)
    {
        if (cards[indexCursor[index]].selected)
            return;
        
        indexCursor[index] = (cards.Count + indexCursor[index] + step) % cards.Count;
        if (cards[indexCursor[index]].selected)
            indexCursor[index] = (indexCursor[index] + step) % cards.Count;
        switchAudio.Play();
        movementCursor[index].MoveTo(cards[indexCursor[index]].transform.localPosition, movementDuration);
    }
}