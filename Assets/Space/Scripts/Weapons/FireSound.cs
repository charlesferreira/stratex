using UnityEngine;

public class FireSound : MonoBehaviour {

    public ProjectileInfo info;

    void Start () {

        Destroy(gameObject, info.lifeTime);
    }
}
