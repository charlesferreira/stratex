using System.Collections.Generic;
using UnityEngine;

public class ShipParticles : MonoBehaviour {

    [Header("Thrusters")]
    public List<ParticleSystem> neutralEngine = new List<ParticleSystem>();
    public List<ParticleSystem> primaryEngine = new List<ParticleSystem>();
    public List<ParticleSystem> reserveEngine = new List<ParticleSystem>();

    void Awake() {
        SetEnabled(neutralEngine, true);
        SetEnabled(primaryEngine, false);
        SetEnabled(reserveEngine, false);
    }

    public void SetEnabled(List<ParticleSystem> particles, bool enabled) {
        foreach (var system in particles) {
            var emission = system.emission;
            emission.enabled = enabled;
        }
    }

    public void Enable(List<ParticleSystem> particles) {
        SetEnabled(particles, true);
    }

    public void Disable(List<ParticleSystem> particles) {
        SetEnabled(particles, false);
    }
}
