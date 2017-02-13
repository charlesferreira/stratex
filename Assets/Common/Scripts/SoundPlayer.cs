using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public AudioSource introMusic;
    public AudioSource gameMusic;
    public AudioSource confirmAudio;
    public AudioSource cancelAudio;

    static SoundPlayer instance;
    public static SoundPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundPlayer>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (!Instance.introMusic.isPlaying)
        {
            Instance.gameMusic.Stop();
            Instance.introMusic.Play();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayIntroMusic()
    {
        gameMusic.Stop();
        introMusic.Play();
    }

    public void PlayGameMusic()
    {
        introMusic.Stop();
        gameMusic.Play();
    }
}
