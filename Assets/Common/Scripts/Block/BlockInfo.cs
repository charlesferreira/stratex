using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Common/Block")]
public class BlockInfo : ScriptableObject {

    public BlockColor color;
    public Sprite puzzleSprite;
    public Color realColor;

    [Header("Power Up")]
    public int powerX3;
    public int powerX4;
    public int powerX5;
    public int powerX6;
    public int powerX7;

    Dictionary<int, int> power = new Dictionary<int, int>();

    void OnEnable() {
        power[3] = powerX3;
        power[4] = powerX4;
        power[5] = powerX5;
        power[6] = powerX6;
        power[7] = powerX7;
    }

    public int Power(int size) {
        return power[size];
    }
}
