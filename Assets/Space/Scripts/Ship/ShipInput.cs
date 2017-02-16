using UnityEngine;

public class ShipInput : MonoBehaviour {

    public bool usingLegacyControls;

    public bool Thrusting { get { return Enabled && Input.GetButton(Joystick.AButton); } }
    public bool Fire1 { get { return Enabled && Input.GetButton(Joystick.XButton); } }
    public bool Fire2 { get { return Enabled && Input.GetButtonDown(Joystick.BButton); } }

    public Joystick Joystick { get; set; }

    bool Enabled {
        get {
            return enabled && !(damage.enabled && damage.Flashing);
        }
    }

    ShipDamage damage;

    void Start() {
        damage = GetComponent<ShipDamage>();
    }

    public Vector2 SteeringTarget {
        get {
            if (!Enabled)
                return Vector2.zero;

            return new Vector2(
                Input.GetAxis(Joystick.Horizontal),
                Input.GetAxis(Joystick.Vertical)
            );
        }
    }

    public float LegacySteering {
        get {
            return Enabled ? Input.GetAxis(Joystick.Horizontal) : 0;
        }
    }
}
