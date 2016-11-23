public interface IDominationAreaState {

    void OnStateEnter();

    void OnStateExit();

    void ShipHasEntered(TeamFlags team);

    void ShipHasLeft(TeamFlags team);

    void Update();
}
