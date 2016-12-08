using System;
using UnityEngine;

[Serializable]
public struct Match {
    public BlockInfo info;

    [Range(1, 7)]
    public int size;

    public Match (BlockInfo info, int size) {
        this.info = info;
        this.size = size;
    }
}
