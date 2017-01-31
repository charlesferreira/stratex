using UnityEngine;
using DominationAreaStates;

public class DominationArea : MonoBehaviour {

    // Inspector

    [Header("General")]
    public SpriteRenderer sprite;
    public float pointDuration;

    [Header("Rotator")]
    public Transform rotator;
    public float rotatorSpeed;

    [Header("Cold State")]
    public Color coldColor;

    [Header("Warming Up State")]
    public float pointsTillWarmUp;

    [Header("Hot State")]
    public float hotGlowSpeed;
    public Color hotGlowColor;

    [Header("Cooling Down State")]
    public float pointsTillCoolDown;

    [Header("Overheated State")]
    public float OverheatedBlinkSpeed;
    public ColorRange overheatedColorRange;


    // Properties

    public TeamFlags CurrentTeam { get; private set; }

    public Color Color {
        get { return sprite.material.color; }
        set { sprite.material.color = value; }
    }

    public float HotGlowTime { get { return pointDuration / (hotGlowSpeed * 2f); } }

    public float OverheatedBlinkTime { get { return pointDuration / (OverheatedBlinkSpeed * 2f); } }

    public float TimeToWarmUp { get { return pointsTillWarmUp * pointDuration; } }

    public float TimeToCoolDown { get { return pointsTillCoolDown * pointDuration; } }


    // States
                      
    [HideInInspector] public IDominationAreaState coldState;
    [HideInInspector] public IDominationAreaState warmingUpState;
    [HideInInspector] public IDominationAreaState hotState;
    [HideInInspector] public IDominationAreaState overheatedState;
    [HideInInspector] public IDominationAreaState coolingDownState;
    
    IDominationAreaState currentState;


    // Methods

    void Awake() {
        coldState        = new ColdState(this);
        warmingUpState   = new WarmingUpState(this);
        hotState         = new HotState(this);
        overheatedState  = new OverheatedState(this);
        coolingDownState = new CoolingDownState(this);

        SetState(coldState);
    }

    void Update() {
        currentState.Update();
        rotator.Rotate(Vector3.forward, rotatorSpeed * Time.deltaTime * Time.timeScale);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null) return;

        var team = id.info;

        // Se já contou a entrada da nave, não informa o estado
        if ((CurrentTeam & team.flag) != 0) return;

        CurrentTeam |= team.flag;
        currentState.ShipHasEntered(team);
    }

    void OnTriggerExit2D(Collider2D other) {
        var team = other.GetComponent<TeamIdentity>();
        if (team == null)
            return;
        var teamInfo = team.info;
        
        // Se já contou a saída da nave, não informa o estado
        if ((CurrentTeam & ~teamInfo.flag) == CurrentTeam) return;

        CurrentTeam &= ~teamInfo.flag;
        currentState.ShipHasLeft(teamInfo);
    }

    public void SetState(IDominationAreaState state) {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        currentState.OnStateEnter();
    }
}
