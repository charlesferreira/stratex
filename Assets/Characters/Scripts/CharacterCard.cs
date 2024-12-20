﻿using UnityEngine;

public class CharacterCard : MonoBehaviour {

    [HideInInspector] public bool selected = false;
    [HideInInspector] public Joystick joystick;
    public bool isPilot;

    public float flashDuration = 0.4f;

    public Color finalColor;

    bool flashing = false;

    AudioSource confirmAudio;
    AudioSource cancelAudio;

    void Start () {

        confirmAudio = GetComponents<AudioSource>()[0];
        cancelAudio = GetComponents<AudioSource>()[1];
    }
	
	void Update () {
	
	}

    public void SetSelected(Joystick joystick)
    {
        this.joystick = joystick;
        selected = true;
        GetComponentInChildren<FlashSprite>().StartFlash();
        flashing = true;
        confirmAudio.Play();
        Invoke("DisableCard", flashDuration);
    }

    public void DisableCard()
    {
        flashing = false;
        GetComponentInChildren<FlashSprite>().StopFlash();
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().color = finalColor;

        if (isPilot)
            TeamsSelectionManager.Instance.SetPilot(joystick);
        else
            TeamsSelectionManager.Instance.SetEngineer(joystick);
    }

    internal void Deselect()
    {
        if (flashing)
            return;        
        selected = false;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        GetComponentInChildren<MeshRenderer>().enabled = true;
        cancelAudio.Play();
        TeamsSelectionManager.Instance.UnsetCharacter();
    }
}
