using UnityEngine;
using System.Collections;

public class MainMenuInput : MonoBehaviour {

    [HideInInspector]
    public Joystick joystick;

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

    void Update()
    {

        Up = Down = Left = Right = false;

        if (Input.GetAxisRaw(joystick.Vertical) > cursorThreshold) Up = true;
        if (Input.GetAxisRaw(joystick.Vertical) < -cursorThreshold) Down = true;
        if (Input.GetAxisRaw(joystick.Horizontal) > cursorThreshold) Right = true;
        if (Input.GetAxisRaw(joystick.Horizontal) < -cursorThreshold) Left = true;

        if (Up || Down || Right || Left)
        {
            if (currentCooldown == firstCooldown)
            {
                currentCooldown -= Time.fixedDeltaTime;
                return;
            }
            if (currentCooldown > 0)
            {
                Up = Down = Left = Right = false;
                currentCooldown -= Time.fixedDeltaTime;
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

    public bool StartButton { get { return Input.GetButtonDown(joystick.StartButton); } }
    public bool ConfirmButton { get { return Input.GetButtonDown(joystick.AButton); } }
    public bool CancelButton { get { return Input.GetButtonDown(joystick.BButton); } }
}
