using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CursorManager : MonoBehaviour {

    MenuInput menuInput1;
    MenuInput menuInput2;

    public GameObject cursor1;
    public GameObject cursor2;

    public float movementDuration = .1f;

    Movement movementCursor1;
    Movement movementCursor2;

    TeamCursor teamCursor1;
    TeamCursor teamCursor2;

    public List<TeamCard> cards;

    int indexCursor1;
    int indexCursor2;

    bool sameIndex = false;

    void Start () {

        menuInput1 = GetComponents<MenuInput>()[0];
        menuInput2 = GetComponents<MenuInput>()[1];

        movementCursor1 = cursor1.GetComponent<Movement>();
        movementCursor2 = cursor2.GetComponent<Movement>();

        teamCursor1 = cursor1.GetComponent<TeamCursor>();
        teamCursor2 = cursor2.GetComponent<TeamCursor>();

        indexCursor1 = 0;
        indexCursor2 = cards.Count - 1;

        cursor1.transform.position = cards[indexCursor1].transform.position;
        cursor2.transform.position = cards[indexCursor2].transform.position;
    }

    void Update ()
    {
        CheckInputs(cursor1, menuInput1, movementCursor1, movementCursor2, ref indexCursor1, ref indexCursor2);
        CheckInputs(cursor2, menuInput2, movementCursor2, movementCursor1, ref indexCursor2, ref indexCursor1);
    }

    private void CheckInputs(GameObject cursor, MenuInput menuInput, Movement movementCursor, Movement movementCursorOther, ref int indexCursor, ref int indexCursorOther)
    {
        if (menuInput.Right)
        {
            indexCursor = CheckIndexCursor(movementCursor, ref indexCursor, 1);
        }
        else if (menuInput.Left)
        {
            indexCursor = CheckIndexCursor(movementCursor, ref indexCursor, -1);
        }
        if (menuInput.Confirm)
        {
            cards[indexCursor].ShowCharacters(menuInput.joysticks, cursor);

            if (indexCursorOther == indexCursor)
            {
                ShowSecondCursor();
                if (indexCursorOther == cards.Count - 1)
                    indexCursorOther = (cards.Count + indexCursorOther - 1) % cards.Count;
                else
                    indexCursorOther = (indexCursorOther + 1) % cards.Count;
                movementCursorOther.MoveTo(cards[indexCursorOther].transform.localPosition, movementDuration);
            }
        }
    }

    private int CheckIndexCursor(Movement movementCursor, ref int indexCursor, int step)
    {
        if (cards[indexCursor].selected)
            return indexCursor;
        
        indexCursor = (cards.Count + indexCursor + step) % cards.Count;
        if (cards[indexCursor].selected)
            indexCursor = (indexCursor + step) % cards.Count;
        movementCursor.MoveTo(cards[indexCursor].transform.localPosition, movementDuration);
        Invoke("CheckSameSelection", movementDuration);
        return indexCursor;
    }

    void CheckSameSelection()
    {
        if (indexCursor1 == indexCursor2 && !sameIndex)
        {
            HideSecondCursor();
        }
        else if (sameIndex)
        {
            ShowSecondCursor();
        }
    }

    private void ShowSecondCursor()
    {
        // Ativar cursor 2
        teamCursor1.colors.RemoveRange(2, 2);
        teamCursor1.ChangeColor();
        cursor2.SetActive(true);
        sameIndex = false;
    }

    private void HideSecondCursor()
    {
        // Desativar cursor 2
        teamCursor1.colors.AddRange(teamCursor2.colors);
        cursor2.SetActive(false);
        sameIndex = true;
    }
}
