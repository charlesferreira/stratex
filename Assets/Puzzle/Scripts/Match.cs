using System;
using UnityEngine;

[Serializable]
public struct Match {
    public BlockColor color;

    [Range(1, 7)]
    public int size;

    public Match (BlockColor color, int size) {
        this.color = color;
        this.size = size;
    }
}
