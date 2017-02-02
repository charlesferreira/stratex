﻿using UnityEngine;

public class SoundFX : MonoBehaviour {

    [Range(0, 3)]
    public float pitchRange;

    void Start() {
        var audio = GetComponent<AudioSource>();
        audio.pitch += Random.Range(-pitchRange, pitchRange) / 2f;

        var duration = audio.clip.length / audio.pitch;
        Destroy(gameObject, duration);
    }

}
