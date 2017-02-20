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

    [SerializeField] string startButton;


    public string AButton { get { return aButton; } }
    public string BButton { get { return bButton; } }
    public string XButton { get { return xButton; } }
    public string YButton { get { return yButton; } }
    public string StartButton { get { return startButton; } }
    public string Horizontal { get { return horizontal; } }
    public string Vertical { get { return vertical; } }

    public void Incorporate(Joystick other) {
        horizontal = other.horizontal;
        vertical = other.vertical;
        aButton = other.aButton;
        bButton = other.bButton;
        xButton = other.xButton;
        yButton = other.yButton;
        startButton = other.startButton;
    }

}
