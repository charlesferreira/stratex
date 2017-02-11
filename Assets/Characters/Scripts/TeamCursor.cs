using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TeamCursor : MonoBehaviour {

    public List<Color> colors;
    int colorIndex = 0;

    [Range(0.1f, 1f)]
    public float timeInterval = 0.2f;
    float elapsedTime = 0;

    SpriteRenderer sprite;

	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = colors[colorIndex];
    }

    void Update () {

        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeInterval)
        {
            ChangeColor();
            elapsedTime -= timeInterval;
        }
	}

    public void ChangeColor()
    {
        colorIndex++;
        colorIndex = (colorIndex % colors.Count);
        sprite.color = colors[colorIndex];
    }
}
