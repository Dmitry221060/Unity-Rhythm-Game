using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LeaderboardDTO {
    public List<LeaderboardEntry> records;
}

[Serializable]
public class LeaderboardEntry {
    public string name;
    public int score;
}
