using UnityEngine;

[CreateAssetMenu(menuName = "Common/Team")]
public class TeamInfo : ScriptableObject {

    public TeamFlags flag;

    public Color color;

    public Sprite teamCard;
}
