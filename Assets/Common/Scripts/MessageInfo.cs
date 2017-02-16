using UnityEngine;

[CreateAssetMenu(menuName = "Common/Message")]
public class MessageInfo : ScriptableObject {
    public float startingX;
    [Range(0, 1)]
    public float fadeIn;
    [Range(0, 5)]
    public float slideIn;
    [Range(0, 5)]
    public float stay;
    [Range(0, 5)]
    public float slideOut;
    [Range(0, 1)]
    public float fadeOut;

    public float TotalDuration {
        get {
            return fadeIn + slideIn + stay + slideOut + fadeOut;
        }
    }
}