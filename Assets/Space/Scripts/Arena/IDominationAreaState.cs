public interface IDominationAreaState {

    // State Transitions

    void ToColdState();

    void ToWarmingUpState(TeamInfo team);

    void ToHotState(TeamInfo team);

    void ToOverheatedState();

    void ToCoolingDownState();

    // Callbacks

    void OnStateEnter();

    void OnStateExit();

    // Messages

    void ShipHasEntered(TeamInfo team);

    void ShipHasLeft(TeamInfo team);

    void Update();
}
