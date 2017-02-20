using UnityEngine;
using System.Collections.Generic;

public class MultiInputSimulator : MonoBehaviour {

    public Joystick theOneToRuleThemAll;
    public List<Joystick> virtualJoysticks = new List<Joystick>();

    Dictionary<Joystick, Joystick> possessed = new Dictionary<Joystick, Joystick>();

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    
	void Start() {
        foreach (var bilbo in virtualJoysticks)
            Possess(bilbo);
    }

    void OnDestroy() {
        foreach (var bilbo in virtualJoysticks)
            Release(bilbo);
    }

    void Possess(Joystick bearer) {
        var possessed = ScriptableObject.CreateInstance<Joystick>();
        possessed.Incorporate(bearer);
        this.possessed.Add(bearer, possessed);

        bearer.Incorporate(theOneToRuleThemAll);
    }

    void Release(Joystick bearer) {
        bearer.Incorporate(possessed[bearer]);
    }
}
