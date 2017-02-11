public interface IDominationAreaState {
    // Callbacks
    void OnStateEnter(DominationArea dominationArea);
    void OnStateExit(DominationArea dominationArea);

    // Messages
    void ShipHasEntered(DominationArea dominationArea, TeamInfo team);
    void ShipHasLeft(DominationArea dominationArea, TeamInfo team);
    void Update(DominationArea dominationArea);
}
