using UnityEngine;

public class PuzzleController : MonoBehaviour {

    PuzzleInput input;
    Cursor cursor;

	void Start () {
        input = GetComponent<PuzzleInput>();
        cursor = GetComponentInChildren<Cursor>();
    }
	
	void Update () {
        if (Pause.Instance.pause) return;

        if (input.Up) cursor.Move(CursorMovement.Up);
        if (input.Down) cursor.Move(CursorMovement.Down);
        if (input.Left) cursor.Move(CursorMovement.Left);
        if (input.Right) cursor.Move(CursorMovement.Right);

        if (input.SwapRight) cursor.Swap(SwapDirection.Right);
    }
}
