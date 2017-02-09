using System;

[Serializable]
public class Team {

    public TeamInfo info;

    public int Points { get; private set; }

    public Team(TeamInfo info) {
        this.info = info;
    }

    public int Score() {
        return ++Points;
    }

}
