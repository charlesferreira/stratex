using UnityEngine;
using UnityEngine.UI;

public class TeamSetup : MonoBehaviour {

    public TeamFlags flag;
    
    [Header("Instructions")]
    public SpriteRenderer instructionsPilot;
    public SpriteRenderer instructionsEngineer;

    [Header("HUD")]
    public RawImage teamName;

    [Header("Ship")]
    public ShipEngine engine;
    public Transform shipModelPlaceholder;

    void Start() {
        var info = TeamsManager.Instance.GetTeamInfo(flag);

        // instructions
        instructionsPilot.sprite = info.instructionsPilot;
        instructionsEngineer.sprite = info.instructionsEngineer;

        // HUD
        teamName.texture = info.teamName;

        // ship
        CreateShipModel(info);
    }

    void CreateShipModel(TeamInfo info) {
        var prefab = info.shipModelPrefab;
        var model = (GameObject)Instantiate(
            prefab,
            shipModelPlaceholder.position,
            shipModelPlaceholder.rotation * prefab.transform.rotation,
            shipModelPlaceholder);
        
        engine.Particles = model.GetComponent<ShipParticles>();
    }
}
