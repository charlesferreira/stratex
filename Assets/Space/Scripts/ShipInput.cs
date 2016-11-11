using UnityEngine;

[CreateAssetMenu(menuName = "Space/Input")]
public class ShipInput : ScriptableObject {

    public bool useAlternativeControls;
    public string Alt { get { return useAlternativeControls ? " Alt" : ""; } }

    public string steering;
    public float Steering { get { return Input.GetAxis(steering + Alt); } }

    public string thruster;
    public bool Thruster { get { return Input.GetButton(thruster + Alt); } }

    public string fire1;
    public bool Fire1 { get { return Input.GetButtonDown(fire1 + Alt); } }

    public string fire2;
    public bool Fire2 { get { return Input.GetButtonDown(fire2 + Alt); } }

    public string showHud;
    public bool ShowHud { get { return Input.GetButtonDown(showHud + Alt); } }
}
