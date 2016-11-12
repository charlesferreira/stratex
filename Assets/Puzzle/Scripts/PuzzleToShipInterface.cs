using UnityEngine;

public class PuzzleToShipInterface : MonoBehaviour {

    //public Transform alliedShip;

    public void NotifyMatch(int color, int size)
    {
        Debug.Log("Combinou " + size + " peças de Cor(" + color + ")");
    }
}
