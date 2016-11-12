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

    [Header("Alternative Controls")]
    [SerializeField] bool useAlternativeControls;

    // Propriedades consideram o sufixo "Alt" para controles alternativos
    string Alt { get { return useAlternativeControls ? " Alt" : ""; } }
    public string AButton { get { return aButton + Alt; } }
    public string BButton { get { return bButton + Alt; } }
    public string XButton { get { return xButton + Alt; } }
    public string YButton { get { return yButton + Alt; } }
    public string Horizontal { get { return horizontal + Alt; } }
    public string Vertical { get { return vertical + Alt; } }

}
