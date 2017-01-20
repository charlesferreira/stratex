using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResultManager : MonoBehaviour, IScoreObserver {

    public float delayShowMenu = 1;

    public Sprite finalWinImage;
    public Sprite finalLoseImage;

    public Image finalImageTeam1;
    public Image finalImageTeam2;

    public GameObject resultPanel;
    public GameObject resultMenu;

    // Use this for initialization
    void Start () {
        TeamsManager.Instance.RegisterObserver(this);
        resultPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetWinner(TeamFlags winner)
    {
        if (winner == TeamFlags.Team1)
        {
            finalImageTeam1.sprite = finalWinImage;
            finalImageTeam2.sprite = finalLoseImage;
        }
        else
        {
            finalImageTeam1.sprite = finalLoseImage;
            finalImageTeam2.sprite = finalWinImage;
        }

        resultPanel.SetActive(true);
    }

    public void ScoreHasChanged(TeamFlags flag, int score)
    {
        if (score == 0)
        {
            Time.timeScale = 0;
            Invoke("ShowMenu", delayShowMenu);
            SetWinner(flag);
        }
    }

    void ShowMenu()
    {
        resultMenu.SetActive(true);
    }
}
