using System;

[Serializable]
public class Team {

    public TeamInfo info;

    public int Points { get; private set; }

    public Team(TeamInfo info, int startingPoints) {
        this.info = info;
        Points = startingPoints;
    }

    public int Score() {
        if (Points > 0)
            Points--;

        return Points;
    }

}
