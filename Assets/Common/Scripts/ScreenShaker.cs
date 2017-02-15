using UnityEngine;
using System.Collections.Generic;

public class ScreenShaker : MonoBehaviour {

    public Transform shaker;

    List<Tremor> tremors = new List<Tremor>();
    List<Tremor> tremorsDone = new List<Tremor>();

    void Update() {
        var position = Vector2.zero;
        foreach (var tremor in tremors) {
            position += tremor.Shake();
            if (tremor.Done)
                tremorsDone.Add(tremor);
        }
        shaker.localPosition = position;

        RemoveTremorsDone();
    }

    public void Shake(Tremor tremor) {
        tremors.Add(Tremor.Factory(tremor));
    }

    private void RemoveTremorsDone() {
        foreach (var tremor in tremorsDone)
            tremors.Remove(tremor);
        tremorsDone.Clear();
    }
}
