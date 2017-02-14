using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuInput : MonoBehaviour
{

    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Confirm { get; set; }
    public bool Cancel { get; set; }
    public bool Start { get; set; }

    [Header("References")]

    public List<Joystick> joysticks;

    [Header("Tunning")]
    [Range(1f, 60f)]
    public float cursorMovementsPerSecond = 6;
    [Range(0.1f, 1f)]
    public float firstCooldown = 0.5f;
    [Range(0f, 1f)]
    public float cursorThreshold = 0.65f;

    enum Direction { Up, Down, Left, Right, Center }
    float currentCooldown;
    float DefaultCooldown { get { return 1f / cursorMovementsPerSecond; } }

    void Update()
    {

        Up = Down = Left = Right = Confirm = Cancel = Start = false;

        foreach (var joystick in joysticks)
        {
            if (Input.GetAxisRaw(joystick.Vertical) > cursorThreshold) Up = true;
            if (Input.GetAxisRaw(joystick.Vertical) < -cursorThreshold) Down = true;
            if (Input.GetAxisRaw(joystick.Horizontal) > cursorThreshold) Right = true;
            if (Input.GetAxisRaw(joystick.Horizontal) < -cursorThreshold) Left = true;
            if (Input.GetButtonDown(joystick.AButton)) Confirm = true;
            if (Input.GetButtonDown(joystick.BButton)) Cancel = true;
            if (Input.GetButtonDown(joystick.StartButton)) Start = true;
        }

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

    public void ResetInputs()
    {
        Up = Down = Left = Right = Confirm = Cancel = Start = false;
    }
}
