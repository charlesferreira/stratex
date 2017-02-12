using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamCursor : MonoBehaviour {

    public List<Color> colors;
    int colorIndex = 0;

    [Range(0.1f, 1f)]
    public float timeInterval = 0.2f;
    float elapsedTimeColor = 0;
    float elapsedTimeZPosition = 0;
    public bool isFirstCursor = false;
    bool positiveZ = true;
    bool changeZPosition = false;

    SpriteRenderer sprite;

	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = colors[colorIndex];
    }

    void Update () {

        elapsedTimeColor += Time.deltaTime;
        elapsedTimeZPosition += Time.deltaTime;

        if (elapsedTimeColor > timeInterval)
        {
            ChangeColor();
            elapsedTimeColor -= timeInterval;
        }
        if (!isFirstCursor)
            return;

        if (elapsedTimeZPosition > timeInterval * 2)
        {
            ChangeZPosition();
            elapsedTimeZPosition -= timeInterval * 2;
        }
    }

    private void ChangeZPosition()
    {
        if (positiveZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        positiveZ = !positiveZ;
    }

    public void ChangeColor()
    {
        colorIndex++;
        colorIndex = (colorIndex % colors.Count);
        sprite.color = colors[colorIndex];
    }
}
