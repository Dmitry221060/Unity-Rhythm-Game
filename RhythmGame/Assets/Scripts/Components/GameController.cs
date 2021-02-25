using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public int BPS = 24;
    public int earlyHitLoyalty = 4;
    public int lateHitLoyalty = 4;
    public float waitBeforeStart = 3f;
    public const int beatsInTrack = 32;
    public string level = "Butterfly";
    public int score { get; private set; } = 0;
    public int lifes { get; private set; } = 3;

    private GameObject timer;
    private ScoreBar scoreBar;
    private AudioSource audioSource;
    private LevelManagerClass levelManager;

    private IEnumerator Start() {
        timer = GameObject.FindGameObjectWithTag("Timer");
        scoreBar = GameObject.FindGameObjectWithTag("ScoreBar").GetComponent<ScoreBar>();
        audioSource = GetComponent<AudioSource>();
        levelManager = new LevelManagerClass(this);

        yield return LoadLevel();
        yield return StartGame();
    }

    private IEnumerator LoadLevel() {
        levelManager.LoadLevel(level);
        audioSource.clip = levelManager.audioClip;

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator StartGame() {
        scoreBar.SetScore(score);
        scoreBar.SetLifes(lifes);
        yield return Countdown();

        audioSource.Play();
        InvokeRepeating("GameLoop", 0f, 1f / BPS);
    }

    private IEnumerator Countdown() {
        float fractionalPart = waitBeforeStart % 1;
        if (fractionalPart != 0)
            yield return new WaitForSeconds(fractionalPart);

        TMP_Text timerTextComponent = timer.GetComponent<TMP_Text>();

        int timeLeft = (int)(waitBeforeStart - fractionalPart);
        while (timeLeft > 0) {
            if (timeLeft <= 3) timerTextComponent.text = timeLeft.ToString();

            timeLeft--;
            yield return new WaitForSeconds(1f);
        }

        timerTextComponent.text = "";
        yield return null;
    }

    private void GameLoop() {
        if (lifes == 0) return;
        Beat();
        scoreBar.SetScore(score);
        scoreBar.SetLifes(lifes);
    }

    private IEnumerator levelManagerIterator;
    private void Beat() {
        if (levelManagerIterator == null) {
            levelManagerIterator = levelManager.Beat();
        }

        levelManagerIterator.MoveNext();
    }

    public void onMissNote() {
        if (lifes <= 0) return;
        lifes -= 1;

        if (lifes == 0) GameOver();
    }

    public void onHitNote() {
        score += 10;
    }

    public void onEndOfTrackMap() {
        GameOver();
    }

    private void GameOver() {
        CancelInvoke("GameLoop");
        levelManager.DestroyAllNotes();
        audioSource.Stop();

        SceneManager.LoadScene("ScoreScreen", LoadSceneMode.Additive);
    }
}
