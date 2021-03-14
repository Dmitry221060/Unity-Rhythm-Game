using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour {
    public GameObject score;
    public GameObject lives;
    public string scorePrefix = "Score: ";
    public string livesPrefix = "Lives left: ";

    private TMP_Text scoreTextComponent;
    private TMP_Text livesTextComponent;

    private void Start() {
        scoreTextComponent = score.GetComponent<TMP_Text>();
        livesTextComponent = lives.GetComponent<TMP_Text>();
    }

    public void SetScore(int score) {
        scoreTextComponent.text = scorePrefix + score.ToString();
    }

    public void SetLifes(int lifes) {
        livesTextComponent.text = livesPrefix + lifes.ToString();
    }
}
