using UnityEngine;

public class Movement : MonoBehaviour {

    public MovementType movementType;

    Vector3 target;
    Vector3 startLocalPosition;
    float elapsedTime;
    bool isMoving = false;
    float duration;
    float partial;
    float waitingTime;

    void Update () {

        if (isMoving)
        {
            if (waitingTime > 0)
            {
                waitingTime -= Time.deltaTime;
                return;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                transform.position = target;
                isMoving = false;
                return;
            }

            partial = elapsedTime / duration;

            switch (movementType)
            {
                case MovementType.Linear:
                    Linear();
                    break;
                case MovementType.QuadraticIn:
                    QuadraticIn();
                    break;
                case MovementType.QuadraticOut:
                    QuadraticOut();
                    break;
                case MovementType.QuadraticInOut:
                    QuadraticInOut();
                    break;
                default:
                    break;
            }
        }
    }

    private void QuadraticInOut()
    {
        if (partial > 1)
        {
            QuadraticOut();
        }
        else
        {
            QuadraticIn();
        }
    }

    private void QuadraticOut()
    {
        transform.localPosition = startLocalPosition - (target - startLocalPosition) * partial * (partial - 2);
    }

    private void QuadraticIn()
    {
        transform.localPosition = startLocalPosition + (target - startLocalPosition) * partial * partial;
    }

    private void Linear()
    {
        transform.localPosition = startLocalPosition + (target - startLocalPosition) * partial;
    }

    public void MoveTo(Vector3 target, float duration)
    {
        MoveTo(movementType, target, duration);
    }
    public void MoveTo(MovementType movementType, Vector3 target, float duration)
    {
        MoveTo(movementType, target, duration, 0);
    }
    public void MoveTo(Vector3 target, float duration, float waitingTime)
    {
        MoveTo(movementType, target, duration, waitingTime);
    }
    public void MoveTo(MovementType movementType, Vector3 target, float duration, float waitingTime)
    {
        this.movementType = movementType;
        this.target = target;
        this.duration = duration;
        this.waitingTime = waitingTime;

        startLocalPosition = transform.position;
        elapsedTime = 0;
        isMoving = true;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
