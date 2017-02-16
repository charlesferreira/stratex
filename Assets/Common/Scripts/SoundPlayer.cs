using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public bool muteMusic;

    [Header("Music")]
    public AudioSource introMusic;
    public AudioSource gameMusic;
    public AudioSource resultMusic;

    [Header("Menu")]
    public AudioSource menuSwitchAudio;
    public AudioSource confirmAudio;
    public AudioSource cancelAudio;

    AudioSource currentMusic;

    static SoundPlayer instance;
    public static SoundPlayer Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<SoundPlayer>();
            }

            return instance;
        }
    }

    void Awake() {
        DontDestroyOnLoad(Instance.gameObject);
        PlayIntroMusic();
    }

    public static void PlayIntroMusic() {
        if (Instance.introMusic.isPlaying || Instance.muteMusic)
            return;

        StopAll();
        Instance.introMusic.Play();
        Instance.currentMusic = Instance.introMusic;
    }

    public static void PlayGameMusic() {
        StopAll();
        if (Instance.muteMusic) return;

        Instance.gameMusic.Play();
        Instance.currentMusic = Instance.gameMusic;
    }

    public static void PlayResultMusic() {
        StopAll();
        if (Instance.muteMusic) return;

        Instance.resultMusic.Play();
        Instance.currentMusic = Instance.resultMusic;
    }

    public static void StopAll() {
        Instance.introMusic.Stop();
        Instance.gameMusic.Stop();
        Instance.resultMusic.Stop();
    }

    public static void Pause() {
        if (Instance.currentMusic.isPlaying)
            Instance.currentMusic.Pause();
    }

    public static void UnPause() {
        if (!Instance.currentMusic.isPlaying)
            Instance.currentMusic.UnPause();
    }
}
