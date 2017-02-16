using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class TeamsSelectionManager : MonoBehaviour {

    public CharacterSelection team1;
    public CharacterSelection team2;

    int playersReady = 0;

    static TeamsSelectionManager instance;
    public static TeamsSelectionManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TeamsSelectionManager>();
            return instance;
        }
    }

    public void SetTeamInfo(Joystick joystick, TeamInfo teamInfo)
    {
        if (joystick == team1.P1 || joystick == team1.P2)
            team1.teamInfo = teamInfo;
        else
            team2.teamInfo = teamInfo;
    }


    public void SetPilot(Joystick joystick)
    {
        if (joystick == team1.P1 || joystick == team1.P2)
            team1.SetPilotJoystick(joystick);
        else
            team2.SetPilotJoystick(joystick);
        IncrementPlayersReady();
    }

    public void SetEngineer(Joystick joystick)
    {
        if (joystick == team1.P1 || joystick == team1.P2)
            team1.SetEngineerJoystick(joystick);
        else
            team2.SetEngineerJoystick(joystick);
        IncrementPlayersReady();
    }

    private void IncrementPlayersReady()
    {
        playersReady++;
        if (playersReady == 4)
        {
            TeamsManager.Instance.Init(
                team1.teamInfo, team1.Pilot, team1.Engineer, 
                team2.teamInfo, team2.Pilot, team2.Engineer);
            SceneManager.LoadScene(2);
        }
    }

    public void UnsetCharacter()
    {
        playersReady--;
    }
}
