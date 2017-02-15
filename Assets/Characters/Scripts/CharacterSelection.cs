[System.Serializable]
public class CharacterSelection {

    public Joystick P1;
    public Joystick P2;

    public TeamInfo teamInfo;

    public Joystick Pilot;
    public Joystick Engineer;

    public void SetPilotJoystick(Joystick joystick)
    {
        Pilot = joystick;
    }

    public void SetEngineerJoystick(Joystick joystick)
    {
        Engineer = joystick;
    }
}
