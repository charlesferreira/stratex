using UnityEngine;
using System.Collections.Generic;

public class MenuInput : MonoBehaviour
{
    [Header("References")]
    public List<Joystick> joysticks;

    [Header("Tunning")]
    [Range(1f, 60f)]
    public float cursorMovementsPerSecond = 6;
    [Range(0.1f, 1f)]
    public float firstCooldown = 0.5f;
    [Range(0f, 1f)]
    public float cursorThreshold = 0.65f;


    public bool Up { get; private set; }
    public bool Down { get; private set; }
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool Confirm { get; private set; }
    public bool Cancel { get; private set; }
    public bool Start { get; private set; }

    float currentCooldown;
    float DefaultCooldown { get { return 1f / cursorMovementsPerSecond; } }

    void Update() {
        ResetInputs();

        foreach (var joystick in joysticks) {
            if (Input.GetAxisRaw(joystick.Vertical) > cursorThreshold) Up = true;
            if (Input.GetAxisRaw(joystick.Vertical) < -cursorThreshold) Down = true;
            if (Input.GetAxisRaw(joystick.Horizontal) > cursorThreshold) Right = true;
            if (Input.GetAxisRaw(joystick.Horizontal) < -cursorThreshold) Left = true;
            if (Input.GetButtonDown(joystick.AButton)) Confirm = true;
            if (Input.GetButtonDown(joystick.BButton)) Cancel = true;
            if (Input.GetButtonDown(joystick.StartButton)) Start = true;
        }

        if (Up || Down || Right || Left) {
            if (currentCooldown == firstCooldown) {
                currentCooldown -= Time.unscaledDeltaTime;
                return;
            }
            if (currentCooldown > 0) {
                Up = Down = Left = Right = false;
                currentCooldown -= Time.unscaledDeltaTime;
                return;
            }
            currentCooldown += DefaultCooldown;
            return;
        }
        currentCooldown = firstCooldown;
    }

    public void ResetInputs() {
        Up = Down = Left = Right = Confirm = Cancel = Start = false;
    }
}
