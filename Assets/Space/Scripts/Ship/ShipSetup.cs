using UnityEngine;

public class ShipSetup : MonoBehaviour {

    public Transform modelPlaceholder;
    
	void Start () {
        var id = GetComponent<TeamIdentity>();
        var info = TeamsManager.Instance.GetTeamInfo(id.flag);
        var prefab = info.shipModelPrefab;
        var model = (GameObject) Instantiate(
            prefab, 
            modelPlaceholder.position, 
            modelPlaceholder.rotation * prefab.transform.rotation, 
            modelPlaceholder);

        var engine = GetComponent<ShipEngine>();
        engine.Particles = model.GetComponent<ShipParticles>();
    }
	
	void Update () {
	
	}
}
