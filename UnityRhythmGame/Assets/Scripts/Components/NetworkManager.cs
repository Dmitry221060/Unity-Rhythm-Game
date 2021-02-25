using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour {
    public string apiUrl = "http://127.0.0.1:8080";
    private string fetchLeaderboardMethod = "/leaderboard";
    private string shareScoreMethod = "/score";

    public void ShareScore(string level, int score) {
        StartCoroutine(CallShareScore(level, score));
    }

    private IEnumerator CallShareScore(string level, int score) {
        string methodUrl = apiUrl + shareScoreMethod;
        ShareScoreRequest requestData = new ShareScoreRequest(level, score);

        string requestBody = JsonUtility.ToJson(requestData);
        byte[] encodedBody = System.Text.Encoding.UTF8.GetBytes(requestBody);
        UnityWebRequest uwr = UnityWebRequest.Put(methodUrl, encodedBody);
        uwr.SetRequestHeader("content-type", "application/json");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.LogError("Request Error: " + uwr.error);
        }
    }

    public void FetchLeaderboard(string levelName, Action<string, LeaderboardDTO> callback) {
        StartCoroutine(CallFetchLeaderboard(levelName, callback));
    }

    private IEnumerator CallFetchLeaderboard(string levelName, Action<string, LeaderboardDTO> callback) {
        string methodUrl = apiUrl + fetchLeaderboardMethod;
        string requestUrl = methodUrl + $"?level={levelName}";

        UnityWebRequest uwr = UnityWebRequest.Get(requestUrl);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            callback(uwr.error, null);
            yield break;
        }

        string json = uwr.downloadHandler.text;
        LeaderboardDTO data = JsonUtility.FromJson<LeaderboardDTO>(json);
        callback(null, data);
    }
}
