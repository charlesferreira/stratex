﻿using UnityEngine;
using DominationAreaStates;

public class DominationArea : MonoBehaviour {
    
    [Header("References")]
    public SpriteRenderer background;
    public Rotor rotor;
    public Rings rings;
    public float blocksAttraction;
    
    [Header("States")]
    [SerializeField] ColdState coldState;
    [SerializeField] WarmingUpState warmingUpState;
    [SerializeField] HotState hotState;
    [SerializeField] OverheatedState overheatedState;
    [SerializeField] CoolingDownState coolingDownState;
    IDominationAreaState currentState;

    public bool Overheating { get; private set; }

    // Private

    bool idle;

    // Properties

    public Color Color {
        get { return background.color; }
        set { background.color = value; }
    }
    public Color ColdColor { get { return coldState.coldColor; } }
    public TeamFlags CurrentTeam { get; private set; }
    public TeamInfo DominatingTeam { get; private set; }

    // Methods

    public void PlaySound(AudioSource sound) {
        StopSounds();
        sound.Play();
    }

    public void StopSounds() {
        SoundPlayer.Instance.stratexHot.Stop();
        SoundPlayer.Instance.stratexOverheat.Stop();
        SoundPlayer.Instance.stratexWarmingUp.Stop();
    }
    
    void Awake() {
        SetIdle(true);
    }

    void Update() {
        currentState.Update(this);
    }

    void OnTriggerEnter2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var flag = id.flag;

        // Se já contou a entrada da nave, não informa o estado
        if ((CurrentTeam & flag) != 0) return;

        CurrentTeam |= flag;
        currentState.ShipHasEntered(this, flag);
    }

    void OnTriggerExit2D(Collider2D other) {
        var id = other.GetComponent<TeamIdentity>();
        if (id == null)
            return;
        var flag = id.flag;
        
        // Se já contou a saída da nave, não informa o estado
        if ((CurrentTeam & ~flag) == CurrentTeam) return;

        CurrentTeam &= ~flag;
        currentState.ShipHasLeft(this, flag);
    }

    public void SetState(IDominationAreaState state) {
        if (idle) return;

        if (currentState != null)
            currentState.OnStateExit(this);

        currentState = state;
        currentState.OnStateEnter(this);
        Overheating = false;
    }

    public void SetIdle(bool idle) {
        if (idle)
            ToCoolingDownState();
        this.idle = idle;
    }

    public void TurnOff() {
        SetIdle(true);
        StartCoroutine(rotor.Stop());
        StartCoroutine(rings.Stop());
    }

    // State transitions

    public void ToColdState() {
        SetState(coldState);
    }

    public void ToWarmingUpState(TeamFlags team) {
        DominatingTeam = TeamsManager.Instance.GetTeamInfo(team);
        SetState(warmingUpState);
    }

    public void ToHotState() {
        SetState(hotState);
    }

    public void ToOverheatedState() {
        SetState(overheatedState);
        Overheating = true;
    }

    public void ToCoolingDownState() {
        SetState(coolingDownState);
    }
}
