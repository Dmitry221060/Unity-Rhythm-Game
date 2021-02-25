using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ShareScoreRequest {
    public string level;
    public int score;

    public ShareScoreRequest(string level, int score) {
        this.level = level;
        this.score = score;
    }
}
