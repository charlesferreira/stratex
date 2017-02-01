using UnityEngine;

[System.Serializable]
public class Tremor {
    [SerializeField] float time;
    [SerializeField] float strength;

    // todo: implementar damping
    //[Range(0, 1)]
    //[SerializeField] float damping;

    float startingTime;
    float startingStrength;

    public bool Done{ get { return time <= 0;} }

    public static Tremor Factory(Tremor tremor) {
        return new Tremor(tremor);
    }

    private Tremor(Tremor other) {
        time = other.time;
        startingTime = time;
        startingStrength = other.strength;
    }

    public Vector2 Shake() {
        if (Done) return Vector2.zero;

        Update();
        
        var angle = Random.Range(0, 360);
        return new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)) * strength;
    }

    private void Update() {
        time -= Time.deltaTime;
        strength = Mathf.Lerp(0, startingStrength, time / startingTime);
    }
}