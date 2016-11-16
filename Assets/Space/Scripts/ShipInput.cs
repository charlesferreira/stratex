using UnityEngine;

public class ShipInput : MonoBehaviour {

    public Joystick joystick;

    public bool Fire1 { get { return Input.GetButtonDown(joystick.XButton); } }
    public bool Fire2 { get { return Input.GetButtonDown(joystick.BButton); } }
    public bool Thrusting { get { return Input.GetButton(joystick.AButton); } }
    public float Steering { get { return Input.GetAxis(joystick.Horizontal); } }

    public bool Shield { get { return Input.GetButtonDown(joystick.YButton); } }
}
