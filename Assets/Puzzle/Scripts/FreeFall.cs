using UnityEngine;
using System.Collections;

public class FreeFall : MonoBehaviour {

    public float Gravity = 10;

    float waitingTime;

    bool falling;
    float velocity;

	void Update () {
        if (!falling)
            return;

        if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
            return;
        }

        var v = velocity + Gravity * Time.fixedDeltaTime;
        var s = ((v + velocity) / 2f) * Time.fixedDeltaTime;
        velocity = v;
        transform.position += Vector3.down * s;
    }

    public void ToFall() {
        falling = true;
        waitingTime = 0;
    }

    public void ToFall(float waitingTime)
    {
        this.waitingTime = waitingTime;
        falling = true;
    }

    public void Stop() {
        velocity = 0;
        falling = false;
    }
}
