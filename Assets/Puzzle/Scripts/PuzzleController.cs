using UnityEngine;

public class PuzzleController : MonoBehaviour {

    public Transform cursor;

    PuzzleInput input;
    Cursor scriptCursor;

	void Start () {
        input = GetComponent<PuzzleInput>();
        scriptCursor = cursor.GetComponent<Cursor>();

    }
	
	void Update () {
        if (input.Up) scriptCursor.Move(CursorMovement.Up);
        else if (input.Down) scriptCursor.Move(CursorMovement.Down);
        else if (input.Left) scriptCursor.Move(CursorMovement.Left);
        else if (input.Right) scriptCursor.Move(CursorMovement.Right);

        if (input.SwapUp) scriptCursor.Swap(SwapDirection.Up);
        if (input.SwapDown) scriptCursor.Swap(SwapDirection.Down);
        if (input.SwapLeft) scriptCursor.Swap(SwapDirection.Left);
        if (input.SwapRight) scriptCursor.Swap(SwapDirection.Right);
    }
}
