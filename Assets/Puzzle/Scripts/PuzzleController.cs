using UnityEngine;

public class PuzzleController : MonoBehaviour {

    public Transform cursor;

    PuzzleInput input;

	void Start () {
        input = GetComponent<PuzzleInput>();
	}
	
	void Update () {

        if (input.Up) cursor.GetComponent<Cursor>().Move(CursorMovement.Up);
        else if (input.Down) cursor.GetComponent<Cursor>().Move(CursorMovement.Down);
        else if (input.Left) cursor.GetComponent<Cursor>().Move(CursorMovement.Left);
        else if (input.Right) cursor.GetComponent<Cursor>().Move(CursorMovement.Right);

        if (input.SwapUp) print("Trocar com de Cima");
        if (input.SwapDown) print("Trocar com de Baixo");
        if (input.SwapLeft) print("Trocar com da Esquerda");
        if (input.SwapRight) print("Trocar com da Direita");
    }
}
