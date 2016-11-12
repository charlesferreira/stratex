using UnityEngine;

public class PuzzleController : MonoBehaviour {

    PuzzleInput input;

	void Start () {
        input = GetComponent<PuzzleInput>();
	}
	
	void Update () {
        if (input.Up) print("Cima");
        if (input.Down) print("Baixo");
        if (input.Left) print("Esquerda");
        if (input.Right) print("Direita");

        if (input.SwapUp) print("Trocar com de Cima");
        if (input.SwapDown) print("Trocar com de Baixo");
        if (input.SwapLeft) print("Trocar com da Esquerda");
        if (input.SwapRight) print("Trocar com da Direita");
    }
}
