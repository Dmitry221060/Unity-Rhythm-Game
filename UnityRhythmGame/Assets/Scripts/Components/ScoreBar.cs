using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour {
    private TMP_Text scoreTextComponent;
    private TMP_Text lifesTextComponent;

    public string scorePrefix = "Score: ";
    public string lifesPrefix = "Lifes left: ";

    private void Start() {
        scoreTextComponent = transform.GetChild(0).GetComponent<TMP_Text>();
        lifesTextComponent = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetScore(int score) {
        scoreTextComponent.text = scorePrefix + score.ToString();
    }

    public void SetLifes(int lifes) {
        lifesTextComponent.text = lifesPrefix + lifes.ToString();
    }
}
