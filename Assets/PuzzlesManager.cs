using UnityEngine;
using System.Collections.Generic;

public class PuzzlesManager : MonoBehaviour {
    
    public List<BlockInfo> blocksInfo;

    Dictionary<BlockColor, BlockInfo> blocksInfoDictionary;

    static PuzzlesManager instance;
    public static PuzzlesManager Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<PuzzlesManager>();
            return instance;
        }
    }

    void Start () {
        blocksInfoDictionary = new Dictionary<BlockColor, BlockInfo>();
        foreach (var info in blocksInfo) {
            blocksInfoDictionary.Add(info.color, info);
        }
    }

    public BlockInfo GetBlockInfo(BlockColor color) {
        return blocksInfoDictionary[color];
    }
}
