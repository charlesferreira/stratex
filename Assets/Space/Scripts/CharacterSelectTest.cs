using UnityEngine;

public class CharacterSelectTest : MonoBehaviour {

    [Header("Teams")]
    public TeamInfo[] teams;

    [Header("Team 1")]
    public Joystick pilot1;
    public Joystick engineer1;

    [Header("Team 2")]
    public Joystick pilot2;
    public Joystick engineer2;

    void OnEnable () {
        // sorteia os dois times
        int index1 = Random.Range(0, teams.Length);
        int index2 = index1;
        while(index2 == index1) {
            index2 = Random.Range(0, teams.Length);
        }

        // inicia o gerenciador com os times "selecionados"
        TeamsManager.Instance.Init(teams[index1], pilot1, engineer1, teams[index2], pilot2, engineer2);
        gameObject.SetActive(false);
	}
}
