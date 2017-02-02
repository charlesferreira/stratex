using UnityEngine;

public class SoundFX {

    public static GameObject PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitch) {
        var gameObject = new GameObject();
        gameObject.transform.position = position;

        var audio = gameObject.AddComponent<AudioSource>();
        audio.pitch = pitch;
        audio.PlayOneShot(clip, volume);
        Object.Destroy(gameObject, clip.length / pitch + 0.2f);

        return gameObject;
    }

}
