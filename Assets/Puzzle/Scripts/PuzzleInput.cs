using UnityEngine;

public class PuzzleInput : MonoBehaviour {

    [Header("Tunning")]
    [Range(1f, 60f)]
    public float cursorMovementsPerSecond;
    [Range(0.1f, 1f)]
    public float firstCooldown = 0.1f;
    [Range(0f, 1f)]
    public float cursorThreshold = 0.5f;

    enum Direction { Up, Down, Left, Right, Center }
    float currentCooldown;

    float DefaultCooldown { get { return 1f / cursorMovementsPerSecond; } }
    public Joystick Joystick { get; set; }
    
    void Update() {

        Up = Down = Left = Right = false;

        if (Input.GetAxisRaw(Joystick.Vertical) > cursorThreshold) Up = true;
        if (Input.GetAxisRaw(Joystick.Vertical) < -cursorThreshold) Down = true;
        if (Input.GetAxisRaw(Joystick.Horizontal) > cursorThreshold) Right = true;
        if (Input.GetAxisRaw(Joystick.Horizontal) < -cursorThreshold) Left = true;

        if (Up || Down || Right || Left)
        {
            if (currentCooldown == firstCooldown)
            {
                currentCooldown -= Time.deltaTime;
                return;
            }
            if (currentCooldown > 0)
            {
                Up = Down = Left = Right = false;
                currentCooldown -= Time.deltaTime;
                return;
            }
            currentCooldown += DefaultCooldown;
            return;
        }
        currentCooldown = firstCooldown;
    }

    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }

    public bool SwapRight { get { return Input.GetButtonDown(Joystick.BButton)
                                      || Input.GetButtonDown(Joystick.XButton)
                                      || Input.GetButtonDown(Joystick.AButton)
                                      || Input.GetButtonDown(Joystick.YButton); } }
}
