using UnityEngine;

public class PuzzleController : MonoBehaviour {

    public Transform cursor;

    PuzzleInput input;
    Cursor scriptCursor;

    float startDelayTime = 1.8f;

	void Start () {
        input = GetComponent<PuzzleInput>();
        scriptCursor = cursor.GetComponent<Cursor>();
    }
	
	void Update () {

        if (Pause.Instance.pause) return;
        if (startDelayTime > 0)
        {
            startDelayTime -= Time.deltaTime;
            return;
        }

        if (input.Up) scriptCursor.Move(CursorMovement.Up);
        if (input.Down) scriptCursor.Move(CursorMovement.Down);
        if (input.Left) scriptCursor.Move(CursorMovement.Left);
        if (input.Right) scriptCursor.Move(CursorMovement.Right);

        if (input.SwapRight) scriptCursor.Swap(SwapDirection.Right);
    }
}
