using UnityEngine;
using DominationAreaStates;

public class DominationArea : MonoBehaviour {
    
    [Header("References")]
    public SpriteRenderer background;
    public Rotor rotor;
    public Rings rings;
    
    [Header("States")]
    [SerializeField] ColdState coldState;
    [SerializeField] WarmingUpState warmingUpState;
    [SerializeField] HotState hotState;
    [SerializeField] OverheatedState overheatedState;
    [SerializeField] CoolingDownState coolingDownState;
    IDominationAreaState currentState;

    // Properties

    public Color Color {
        get { return background.color; }
        set { background.color = value; }
    }
    public Color ColdColor { get { return coldState.coldColor; } }
    public TeamFlags CurrentTeam { get; private set; }
    public TeamInfo DominatingTeam { get; private set; }

    // Methods
    
    void Awake() {
        SetState(coldState);
    }

    void Update() {
        currentState.Update(this);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var team = id.info;

        // Se já contou a entrada da nave, não informa o estado
        if ((CurrentTeam & team.flag) != 0) return;

        CurrentTeam |= team.flag;
        currentState.ShipHasEntered(this, team);
    }

    void OnTriggerExit2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var team = id.info;
        
        // Se já contou a saída da nave, não informa o estado
        if ((CurrentTeam & ~team.flag) == CurrentTeam) return;

        CurrentTeam &= ~team.flag;
        currentState.ShipHasLeft(this, team);
    }

    public void SetState(IDominationAreaState state) {
        if (currentState != null)
            currentState.OnStateExit(this);

        currentState = state;
        currentState.OnStateEnter(this);
    }

    // State transitions

    public void ToColdState() {
        SetState(coldState);
    }

    public void ToWarmingUpState(TeamInfo team) {
        DominatingTeam = team;
        SetState(warmingUpState);
    }

    public void ToHotState() {
        SetState(hotState);
    }

    public void ToOverheatedState() {
        SetState(overheatedState);
    }

    public void ToCoolingDownState() {
        SetState(coolingDownState);
    }
}
