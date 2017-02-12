using UnityEngine;

public class GameTimer : MonoBehaviour {
    
    [Range(0, 300)]
    public int gameDuration;

    float time;
    bool playing;

    public float TimeLeft { get { return time; } }
    public bool TimeUp { get { return time <= 0; } }

    static GameTimer instance;
    public static GameTimer Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<GameTimer>();
            return instance;
        }
    }

    void Start() {
        Reset();
    }
	
	void Update () {
        if (!playing) return;

        time -= Time.deltaTime;
        if (time <= 0) {
            time = 0;
            Pause();
        }
	}

    public void Play() {
        playing = true;
    }

    public void Pause() {
        playing = false;
    }

    public void Reset() {
        playing = false;
        time = gameDuration;
    }

    public override string ToString() {
        var mins = Mathf.Floor(time / 60);
        var secs = Mathf.Floor(time - (mins * 60));
        return mins + ":" + secs.ToString("00");
    }
}
