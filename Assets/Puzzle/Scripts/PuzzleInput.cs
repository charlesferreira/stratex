using UnityEngine;

public class PuzzleInput : MonoBehaviour {

    [Header("References")]
    public Joystick joystick;

    [Header("Tunning")]
    [Range(1f, 10f)]
    public float cursorMovementsPerSecond;
    [Range(0f, 1f)]
    public float cursorThreshold = 0.5f;

    enum Direction { Up, Down, Left, Right, Center }
    Direction currentDirection = Direction.Center;
    float currentCooldown;
    float DefaultCooldown { get { return 1f / cursorMovementsPerSecond; } }

    void Update() {
        var nextDirection = Direction.Center;
        if (Input.GetAxisRaw(joystick.Vertical) > cursorThreshold) nextDirection = Direction.Up;
        else if (Input.GetAxisRaw(joystick.Vertical) < -cursorThreshold) nextDirection = Direction.Down;
        else if (Input.GetAxisRaw(joystick.Horizontal) > cursorThreshold) nextDirection = Direction.Right;
        else if (Input.GetAxisRaw(joystick.Horizontal) < -cursorThreshold) nextDirection = Direction.Left;

        if (nextDirection != currentDirection)
            currentCooldown = 0f;
        else if (currentCooldown < 0f)
            currentCooldown += DefaultCooldown;

        currentDirection = nextDirection;
        currentCooldown -= Time.deltaTime;
    }

    bool CheckDirection(Direction direction) {
        return direction == currentDirection && currentCooldown <= 0f;
    }

    public bool Up { get { return CheckDirection(Direction.Up); } }
    public bool Down { get { return CheckDirection(Direction.Down); } }
    public bool Left { get { return CheckDirection(Direction.Left); } }
    public bool Right { get { return CheckDirection(Direction.Right); } }

    public bool SwapUp { get { return Input.GetButtonDown(joystick.YButton); } }
    public bool SwapDown { get { return Input.GetButtonDown(joystick.AButton); } }
    public bool SwapLeft { get { return Input.GetButtonDown(joystick.XButton); } }
    public bool SwapRight { get { return Input.GetButtonDown(joystick.BButton); } }

}
