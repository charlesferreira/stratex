using UnityEngine;

public class ShipToPuzzleInterface : MonoBehaviour {

    public Transform alliedPuzzle;

    Grid grid;

    void Start() {
        grid = alliedPuzzle.GetComponent<Grid>();
    }

	public bool NotifyBlockCollected(BlockInfo info) {
        return grid.InsertBlock(info);
    }
}
