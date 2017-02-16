using UnityEngine;

public class GameTimer : MonoBehaviour {

    [Range(1, 300)]
    public int fakeDuration;
    [Range(1, 300)]
    public int realDuration;

    float fakeTime;
    float fakeScale;
    bool playing;

    public float TimeLeft { get { return fakeTime; } }
    public bool TimeUp { get { return fakeTime <= 0; } }

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

        fakeTime -= Time.deltaTime * fakeScale;
        if (fakeTime <= 0) {
            fakeTime = 0;
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
        fakeTime = fakeDuration;
        fakeScale = fakeDuration / (float)realDuration;
    }

    public override string ToString() {
        var mins = Mathf.Floor(fakeTime / 60);
        var secs = Mathf.Floor(fakeTime - (mins * 60));
        return mins + ":" + secs.ToString("00");
    }
}
