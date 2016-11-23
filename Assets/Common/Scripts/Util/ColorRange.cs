using UnityEngine;

[System.Serializable]
public class ColorRange {
    public Color start;
    public Color end;

    public Color Lerp(float t) {
        return Color.Lerp(start, end, t);
    }
}