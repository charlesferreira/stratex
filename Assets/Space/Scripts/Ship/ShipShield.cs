using UnityEngine;

public class ShipShield : MonoBehaviour {

    [Header("References")]
    public GameObject shield;
    public Transform shieldHUD;

    [Header("Duration")]
    public float maxTime;
    [Range(0, 1)]
    public float startingTime;
    
    float time;
    Vector3 hudScale = Vector3.one;

    void Start() {
        time = maxTime * startingTime;
        // Define o escudo na mesma layer que a nave.
        // É isso que permite bloquear os tiros inimigos.
        shield.layer = gameObject.layer;
    }

    void Update() {
        time -= Time.deltaTime;
        time = Mathf.Max(0f, time);
        UpdateHUD();
        // ativa ou desativa o escudo, conforme tenha ou não tempo
        if (shield.activeSelf != time > 0)
            shield.SetActive(!shield.activeSelf);
    }

    void UpdateHUD() {
        hudScale.x = time / maxTime;
        shieldHUD.localScale = hudScale;
    }

    public void AddTime(int time) {
        this.time = Mathf.Min(maxTime, this.time + time);
    }
}
