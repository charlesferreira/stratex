using UnityEngine;

[CreateAssetMenu(menuName = "Common/Input")]
public class Joystick : ScriptableObject {

    [Header("Axis")]
    [SerializeField] string horizontal;
    [SerializeField] string vertical;

    [Header("Buttons")]
    [SerializeField] string aButton;
    [SerializeField] string bButton;
    [SerializeField] string xButton;
    [SerializeField] string yButton;

    public string AButton { get { return aButton; } }
    public string BButton { get { return bButton; } }
    public string XButton { get { return xButton; } }
    public string YButton { get { return yButton; } }
    public string Horizontal { get { return horizontal; } }
    public string Vertical { get { return vertical; } }

}
