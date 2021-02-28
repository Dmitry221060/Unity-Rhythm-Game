using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ScoreScreenController : MonoBehaviour {
    private GameController gameController;
    private NetworkManager networkManager;
    private GameObject scorePanel;
    private GameObject score;
    private GameObject leaderboard;
    private const string cantRecieveLeaderboardMessage = "Error during requesting leaderboard.";

    private void Start() {
        gameController = FindObjectOfType<GameController>();
        networkManager = FindObjectOfType<NetworkManager>();
        scorePanel = GameObject.FindGameObjectWithTag("UIContainer");
        score = scorePanel.transform.Find("ScoreTitle").gameObject;
        leaderboard = scorePanel.transform.Find("Leaderboard").gameObject;

        Init();
    }

    private void Init() {
        networkManager.ShareScore(gameController.levelName, gameController.score);

        TMP_Text scoreTextComponent = score.GetComponent<TMP_Text>();
        TMP_Text leaderboardTextComponent = leaderboard.GetComponent<TMP_Text>();

        scoreTextComponent.text = "Your score: " + gameController.score;
        networkManager.FetchLeaderboard(
            gameController.levelName,
            (string error, LeaderboardDTO data) => {
                if (error != null) {
                    leaderboardTextComponent.text = cantRecieveLeaderboardMessage;
                    Debug.LogError(error);
                    return;
                }

                string leaderboardContent = "";
                for (int i = 0; i < data.records.Count; i++) {
                    LeaderboardEntry entry = data.records[i];
                    leaderboardContent += $"{i + 1}. {entry.name} - {entry.score}\n";
                }
                leaderboardTextComponent.text = leaderboardContent.Trim();
            }
        );
    }

    public void RestartGame() {
        SceneManager.LoadScene("Game");
    }

    public void Exit() {
        Application.Quit();
    }
}
