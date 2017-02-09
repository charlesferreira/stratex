using UnityEngine;
using DominationAreaStates;

public class DominationArea : MonoBehaviour {

    [Header("References")]
    public SpriteRenderer background;
    public Rotor rotor;
    public Rings rings;

    [Header("Dominations Settings")]
    public Color coldColor;
    public float timeToWarmUp;
    public float timeToCoolDown;

    [Header("Overheated Settings")]
    public ColorRange overheatedColorRange;
    public float overheatedBlinkTime;

    public Color Color {
        get { return background.color; }
        set { background.color = value; }
    }

    public TeamFlags CurrentTeam { get; private set; }
    
    [HideInInspector] public IDominationAreaState coldState;
    [HideInInspector] public IDominationAreaState warmingUpState;
    [HideInInspector] public IDominationAreaState hotState;
    [HideInInspector] public IDominationAreaState movingState;
    [HideInInspector] public IDominationAreaState overheatedState;
    [HideInInspector] public IDominationAreaState coolingDownState;
    
    IDominationAreaState currentState;
    
    void Awake() {
        coldState        = new ColdState(this);
        warmingUpState   = new WarmingUpState(this);
        hotState         = new HotState(this);
        movingState      = new MovingState(this);
        overheatedState  = new OverheatedState(this);
        coolingDownState = new CoolingDownState(this);

        SetState(coldState);
    }

    void Update() {
        currentState.Update();
    }

    void OnTriggerEnter2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var team = id.info;

        // Se já contou a entrada da nave, não informa o estado
        if ((CurrentTeam & team.flag) != 0) return;

        CurrentTeam |= team.flag;
        currentState.ShipHasEntered(team);
    }

    void OnTriggerExit2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var team = id.info;
        
        // Se já contou a saída da nave, não informa o estado
        if ((CurrentTeam & ~team.flag) == CurrentTeam) return;

        CurrentTeam &= ~team.flag;
        currentState.ShipHasLeft(team);
    }

    public void SetState(IDominationAreaState state) {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        currentState.OnStateEnter();
    }
}
