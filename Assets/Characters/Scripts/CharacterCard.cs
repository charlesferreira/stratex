using UnityEngine;
using System.Collections;
using System;

public class CharacterCard : MonoBehaviour {

    [HideInInspector] public bool selected = false;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void SetSelected()
    {
        selected = true;
        GetComponentInChildren<SpriteRenderer>().color = new Color(.5f, .5f, .5f, .5f);
    }

    internal void Deselect()
    {
        selected = false;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
