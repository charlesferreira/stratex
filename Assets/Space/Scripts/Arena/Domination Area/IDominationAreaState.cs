public interface IDominationAreaState {
    // Callbacks
    void OnStateEnter(DominationArea dominationArea);
    void OnStateExit(DominationArea dominationArea);

    // Messages
    void ShipHasEntered(DominationArea dominationArea, TeamFlags team);
    void ShipHasLeft(DominationArea dominationArea, TeamFlags team);
    void Update(DominationArea dominationArea);
}
