using UnityEngine;
using System.Collections.Generic;

public class ShipDamage : MonoBehaviour {

    public Transform shipModel;
    public float flashSpeed;

    List<float> damageTimers = new List<float>();
    List<int> finishedTimers = new List<int>();

    SpriteRenderer sr;

    public bool Flashing { get { return damageTimers.Count > 0; } }

    void OnEnable() {
        sr = shipModel.GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        if (!Flashing) {
            sr.material.SetFloat("_FlashAmount", 0);
            return;
        }

        var flash = 0f;
        for (int i = 0; i < damageTimers.Count; i++) {
            damageTimers[i] -= Time.deltaTime;
            if (damageTimers[i] <= 0)
                finishedTimers.Add(i);
            else
                flash += damageTimers[i];
        }
        sr.material.SetFloat("_FlashAmount", Mathf.PingPong(flash * flashSpeed, 1));

        ClearTimers();
    }

    public void AddDamage(float damageTime) {
        damageTimers.Add(damageTime);
    }

    void ClearTimers() {
        for (int i = 0; i < finishedTimers.Count; i++) {
            damageTimers.RemoveAt(finishedTimers[i]);
        }
        finishedTimers.Clear();
    }
}