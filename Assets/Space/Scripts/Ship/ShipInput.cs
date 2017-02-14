using UnityEngine;

public class ShipInput : MonoBehaviour {

    public bool usingLegacyControls;

    public bool Thrusting { get { return enabled && Input.GetButton(joystick.AButton); } }
    public bool Fire1 { get { return enabled && Input.GetButton(joystick.XButton); } }
    public bool Fire2 { get { return enabled && Input.GetButtonDown(joystick.BButton); } }

    Joystick joystick;
    
    void Start() {
        var flag = GetComponent<TeamIdentity>().flag;
        joystick = TeamsManager.Instance.GetEngineer(flag);
    }

    public Vector2 SteeringTarget {
        get {
            if (!enabled)
                return Vector2.zero;

            return new Vector2(
                Input.GetAxis(joystick.Horizontal),
                Input.GetAxis(joystick.Vertical)
            );
        }
    }

    public float LegacySteering {
        get {
            return enabled ? Input.GetAxis(joystick.Horizontal) : 0;
        }
    }
}
