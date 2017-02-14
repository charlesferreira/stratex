using UnityEngine;

[CreateAssetMenu(menuName = "Common/Team")]
public class TeamInfo : ScriptableObject {

    [Header("Prefabs")]
    public GameObject shipModelPrefab;

    [Header("Sprites")]
    public Sprite teamCard;
    public Sprite pilot;
    public Sprite engineer;

    [Header("Colors")]
    public Color neonColor;
    public Color scoreColor;
    public Color bulletColor;
    public Color missileColor;
    public Color stratexColor;

}
