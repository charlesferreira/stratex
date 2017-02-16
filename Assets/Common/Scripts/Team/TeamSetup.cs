using UnityEngine;
using UnityEngine.UI;

public class TeamSetup : MonoBehaviour {

    public TeamFlags flag;
    
    [Header("Instructions")]
    public SpriteRenderer instructionsPilot;
    public SpriteRenderer instructionsEngineer;

    [Header("HUD")]
    public RawImage teamName;
    public SpriteRenderer teamScore;

    [Header("Ship")]
    public ShipInput shipInput;
    public ShipEngine engine;
    public ShipDamage damage;
    public Transform shipModelPlaceholder;

    [Header("Puzzle")]
    public PuzzleInput puzzleInput;

    void Start() {
        var manager = TeamsManager.Instance;
        var info = manager.GetTeamInfo(flag);

        // instructions
        instructionsPilot.sprite = info.instructionsPilot;
        instructionsEngineer.sprite = info.instructionsEngineer;

        // HUD
        teamName.texture = info.teamName;
        teamScore.sprite = info.teamScore;

        // ship
        CreateShipModel(info);
        shipInput.Joystick = manager.GetPilot(flag);
        damage.enabled = enabled;

        // puzzle
        puzzleInput.Joystick = manager.GetEngineer(flag);
    }

    void CreateShipModel(TeamInfo info) {
        // limpa o placeholder
        foreach (Transform child in shipModelPlaceholder.transform) {
            Destroy(child.gameObject);
        }

        // cria o novo model
        var prefab = info.shipModelPrefab;
        var model = (GameObject)Instantiate(prefab,
            shipModelPlaceholder.position,
            shipModelPlaceholder.rotation * prefab.transform.rotation,
            shipModelPlaceholder);
        engine.Particles = model.GetComponent<ShipParticles>();
    }
}
