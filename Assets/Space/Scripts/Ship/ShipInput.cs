using UnityEngine;

public class ShipInput : MonoBehaviour {

    public Joystick joystick;
    public bool usingLegacyControls;

    public bool Thrusting { get { return Input.GetButton(joystick.AButton); } }
    public bool Fire1 { get { return Input.GetButton(joystick.XButton); } }
    public bool Fire2 { get { return Input.GetButtonDown(joystick.BButton); } }
    public bool ShowHUD { get { return Input.GetButton(joystick.YButton); } }

    public Vector2 SteeringTarget {
        get {
            return new Vector2(
                Input.GetAxis(joystick.Horizontal),
                Input.GetAxis(joystick.Vertical)
            );
        }
    }

    public float LegacySteering { get { return Input.GetAxis(joystick.Horizontal); } }
}
