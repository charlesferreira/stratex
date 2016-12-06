using UnityEngine;

public class PuzzleInput : MonoBehaviour {

    [Header("References")]
    public Joystick joystick;

    [Header("Tunning")]
    [Range(1f, 30f)]
    public float cursorMovementsPerSecond;
    [Range(0f, 1f)]
    public float cursorThreshold = 0.5f;

    enum Direction { Up, Down, Left, Right, Center }
    float currentCooldown;
    float DefaultCooldown { get { return 1f / cursorMovementsPerSecond; } }

    void Update() {

        Up = Down = Left = Right = false;

        if (Input.GetAxisRaw(joystick.Vertical) > cursorThreshold) Up = true;
        if (Input.GetAxisRaw(joystick.Vertical) < -cursorThreshold) Down = true;
        if (Input.GetAxisRaw(joystick.Horizontal) > cursorThreshold) Right = true;
        if (Input.GetAxisRaw(joystick.Horizontal) < -cursorThreshold) Left = true;

    }

    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }

    public bool SwapUp { get { return Input.GetButtonDown(joystick.YButton); } }
    public bool SwapDown { get { return Input.GetButtonDown(joystick.AButton); } }
    public bool SwapLeft { get { return Input.GetButtonDown(joystick.XButton); } }
    public bool SwapRight { get { return Input.GetButtonDown(joystick.BButton); } }

}
