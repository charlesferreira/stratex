using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public AudioSource introMusic;
    public AudioSource gameMusic;
    public AudioSource resultMusic;
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
        StopAll();
        introMusic.Play();
    }

    public void PlayGameMusic()
    {
        StopAll();
        gameMusic.Play();
    }

    public void PlayResultMusic()
    {
        StopAll();
        resultMusic.Play();
    }

    void StopAll()
    {
        introMusic.Stop();
        gameMusic.Stop();
        resultMusic.Stop();
    }
}
