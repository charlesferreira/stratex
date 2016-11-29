using UnityEngine;

public class Movement : MonoBehaviour {

    public MovementType MovementType;

    float elapsedTime; // elapsed time
    Vector3 b; // start position
    Vector3 c; // end position
    float d; // total duration
    float t;

    bool isMoving = false;
    float waitingTime;

    void Update() {

        if (isMoving) {
            if (waitingTime > 0) {
                waitingTime -= Time.deltaTime;
                return;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime > d) {
                transform.localPosition = b + c;
                isMoving = false;
                return;
            }
            t = elapsedTime;
            transform.localPosition = CalculatePosition();
        }
    }

    Vector3 CalculatePosition()
    {
        switch (MovementType)
        {
            case MovementType.Linear:
                return Linear();
            case MovementType.InQuad:
                return InQuad();
            case MovementType.OutQuad:
                return OutQuad();
            case MovementType.InOutQuad:
                return InOutQuad();
            case MovementType.InCubic:
                return InCubic();
            case MovementType.OutCubic:
                return OutCubic();
            case MovementType.InOutCubic:
                return InOutCubic();
            case MovementType.InQuart:
                return InQuart();
            case MovementType.OutQuart:
                return OutQuart();
            case MovementType.InOutQuart:
                return InOutQuart();
            case MovementType.InQuint:
                return InQuint();
            case MovementType.OutQuint:
                return OutQuint();
            case MovementType.InOutQuint:
                return InOutQuint();
            case MovementType.InSine:
                return InSine();
            case MovementType.OutSine:
                return OutSine();
            case MovementType.InOutSine:
                return InOutSine();
            case MovementType.InExpo:
                return InExpo();
            case MovementType.OutExpo:
                return OutExpo();
            case MovementType.InOutExpo:
                return InOutExpo();
            case MovementType.InCirc:
                return InCirc();
            case MovementType.OutCirc:
                return OutCirc();
            case MovementType.InOutCirc:
                return InOutCirc();
            case MovementType.OutBounce:
                return OutBounce();
            default:
                throw new System.Exception("MovementType invalid.");
        }
    }

    private Vector3 Linear()
    {
        return c * t / d + b;
    }

    private Vector3 InQuad() {
        return c * (t /= d) * t + b;
    }

    private Vector3 OutQuad()
    {
        return -c * (t /= d) * (t - 2) + b;
    }

    private Vector3 InOutQuad()
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t + b;
        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }

    private Vector3 InCubic()
    {
        return c * (t /= d) * t * t + b;
    }
	private Vector3 OutCubic()
    {
        return c * ((t = t / d - 1) * t * t + 1) + b;
    }
	private Vector3 InOutCubic()
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t + 2) + b;
    }
	private Vector3 InQuart()
    {
        return c * (t /= d) * t * t * t + b;
    }
	private Vector3 OutQuart()
    {
        return -c * ((t = t / d - 1) * t * t * t - 1) + b;
    }
	private Vector3 InOutQuart()
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
        return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
    }
	private Vector3 InQuint()
    {
        return c * (t /= d) * t * t * t * t + b;
    }
	private Vector3 OutQuint()
    {
        return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
    }
	private Vector3 InOutQuint()
    {
        if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
        return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
    }
	private Vector3 InSine()
    {
        return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
    }
	private Vector3 OutSine()
    {
        return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
    }
	private Vector3 InOutSine()
    {
        return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
    }
	private Vector3 InExpo()
    {
        return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
    }
	private Vector3 OutExpo()
    {
        return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
    }
	private Vector3 InOutExpo()
    {
        if (t == 0) return b;
        if (t == d) return b + c;
        if ((t /= d / 2) < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
        return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
    }
	private Vector3 InCirc()
    {
        return -c * (Mathf.Sqrt(1 - (t /= d) * t) - 1) + b;
    }
	private Vector3 OutCirc()
    {
        return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
    }
	private Vector3 InOutCirc()
    {
        if ((t /= d / 2) < 1) return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
        return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
    }

	private Vector3 OutBounce()
    {
        if ((t /= d) < (1 / 2.75))
        {
            return c * (7.5625f * t * t) + b;
        }
        else if (t < (2 / 2.75))
        {
            return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
        }
        else if (t < (2.5 / 2.75))
        {
            return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
        }
        else
        {
            return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }
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

        b = transform.localPosition;
        c = target - b;
        d = duration;
        elapsedTime = 0;

        this.waitingTime = waitingTime;
        isMoving = true;
    }

    public bool IsMoving() {
        return isMoving;
    }
}
