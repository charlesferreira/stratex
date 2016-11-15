using System;
using UnityEngine;

public class ShipShield : MonoBehaviour {

    public float startingTime;

    float time;

    void Start() {

    }

    void Update() {

    }

    public void AddTime(int time) {
        this.time += time;
    }
}
