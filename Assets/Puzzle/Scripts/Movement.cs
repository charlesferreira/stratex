using UnityEngine;

public class Movement : MonoBehaviour {

    public MovementType MovementType;

    Vector3 target;
    Vector3 startLocalPosition;
    float elapsedTime;
    bool isMoving = false;
    float duration;
    float waitingTime;

    void Update() {

        if (isMoving) {
            if (waitingTime > 0) {
                waitingTime -= Time.deltaTime;
                return;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration) {
                transform.localPosition = target;
                isMoving = false;
                return;
            }

            switch (MovementType) {
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

    private void QuadraticInOut() {
        var partial = elapsedTime / (duration / 2);
        if (partial < 1) {
            transform.localPosition = (target - startLocalPosition) / 2 * partial * partial + startLocalPosition;
        }
        else {
            transform.localPosition = startLocalPosition - (target - startLocalPosition) / 2 * partial * ((partial - 2) - 1);
        }
    }

    private void QuadraticIn()
    {
        var partial = elapsedTime / duration;
        transform.localPosition = startLocalPosition + (target - startLocalPosition) * partial * partial;
    }

    private void QuadraticOut() {
        var partial = elapsedTime / duration;
        transform.localPosition = startLocalPosition - (target - startLocalPosition) * partial * (partial - 2);
    }

    private void Linear() {
        var partial = elapsedTime / duration;
        transform.localPosition = startLocalPosition + (target - startLocalPosition) * partial;
    }

    public void MoveTo(Vector3 target, float duration) {
        MoveTo(MovementType, target, duration);
    }

    public void MoveTo(MovementType movementType, Vector3 target, float duration) {
        MoveTo(movementType, target, duration, 0);
    }

    public void MoveTo(Vector3 target, float duration, float waitingTime) {
        MoveTo(MovementType, target, duration, waitingTime);
    }

    public void MoveTo(MovementType movementType, Vector3 target, float duration, float waitingTime) {
        MovementType = movementType;
        this.target = target;
        this.duration = duration;
        this.waitingTime = waitingTime;

        startLocalPosition = transform.localPosition;
        elapsedTime = 0;
        isMoving = true;
    }

    public bool IsMoving() {
        return isMoving;
    }
}
