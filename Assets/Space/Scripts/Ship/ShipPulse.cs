﻿using UnityEngine;

public class ShipPulse : MonoBehaviour {

    [Header("References")]
    public Pulse pulse;

    [Header("Settings")]
    public float minForce;
    public float maxForce;

    Animator anim;
    
    void Start () {
        anim = pulse.GetComponent<Animator>();
        pulse.Init(minForce, maxForce);
    }

    public void Fire(float matchSize) {
        pulse.setMatchSize(matchSize);
        anim.SetTrigger("Pulse");
    }
}
