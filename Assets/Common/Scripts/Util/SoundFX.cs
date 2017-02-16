using UnityEngine;

public class SoundFX : MonoBehaviour {

    [Range(0, 3)]
    public float pitchRange;

    public float Length { get { return GetComponent<AudioSource>().clip.length; } }

    void Start() {
        var audio = GetComponent<AudioSource>();
        audio.pitch += Random.Range(-pitchRange, pitchRange) / 2f;

        var duration = audio.clip.length / audio.pitch;
        Destroy(gameObject, duration);
    }

    public void Play(Vector3 position) {
        Instantiate(this, position, Quaternion.identity);
    }
}
