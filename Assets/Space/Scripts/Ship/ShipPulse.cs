using UnityEngine;

public class ShipPulse : MonoBehaviour {

    [Header("References")]
    public Pulse pulse;

    [Header("Settings")]
    public float minForce;
    public float maxForce;
    public float baseDamageTime;

    [Header("Effects")]
    public Tremor shakeConfig;
    public SoundFX sound;

    Animator anim;
    ScreenShaker screenShaker;

    void Start () {
        anim = pulse.GetComponent<Animator>();
        screenShaker = GetComponent<ScreenShaker>();
        pulse.Init(minForce, maxForce, baseDamageTime);
    }

    public void Fire(float matchSize) {
        pulse.SetMatchSize(matchSize);
        anim.SetTrigger("Pulse");
        screenShaker.Shake(shakeConfig);
        sound.Play(transform.position);
    }
}
