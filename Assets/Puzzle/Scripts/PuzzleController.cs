using UnityEngine;

public class PuzzleController : MonoBehaviour {

    public Transform cursor;

    PuzzleInput input;

	void Start () {
        input = GetComponent<PuzzleInput>();
	}
	
	void Update () {
        var cursorMovement = Vector3.zero;
        if (input.Up) cursorMovement = Vector3.up;
        else if (input.Down) cursorMovement = Vector3.down;
        else if (input.Left) cursorMovement = Vector3.left;
        else if (input.Right) cursorMovement = Vector3.right;

        cursor.Translate(cursorMovement);

        if (input.SwapUp) print("Trocar com de Cima");
        if (input.SwapDown) print("Trocar com de Baixo");
        if (input.SwapLeft) print("Trocar com da Esquerda");
        if (input.SwapRight) print("Trocar com da Direita");
    }
}
