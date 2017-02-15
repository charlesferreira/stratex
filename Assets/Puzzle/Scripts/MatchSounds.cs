using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchSounds : MonoBehaviour {

    public List<AudioSource> sounds;

    public void Play(int matchSize)
    {
        sounds[matchSize - 3].Play();
    }
}
