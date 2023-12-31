using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance { get; private set; }

    public Transform playerListContent;
    public GameObject playerPrefab;

    public TextMeshProUGUI leaderboardNameText;

    public static bool open;

    public bool lerpOpen;
    public float openSpeed = 5f;

    private CanvasGroup cg;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        float alpha = open ? 1.0f : 0.0f;
        cg.alpha = lerpOpen ? Mathf.Lerp(cg.alpha, alpha, Time.deltaTime * openSpeed) : alpha;
        cg.interactable = open;
        cg.blocksRaycasts = open;

        switch (ObstacleSpawner.currentGamemode)
        {
            case GamemodeType.Highscore:
                leaderboardNameText.text = "Leaderboard: " + "Highscore";
                break;
            case GamemodeType.Easy:
                leaderboardNameText.text = "Leaderboard: " + "Easy";
                break;
            case GamemodeType.Hardcore:
                leaderboardNameText.text = "Leaderboard: " + "Rage";
                break;
        }

    }

    public async void ReloadLeaderboard()
    {
        foreach (Transform playerItem in playerListContent)
            if (playerItem.gameObject != playerPrefab)
                Destroy(playerItem.gameObject);

        string leaderboardName = "";
        switch (ObstacleSpawner.currentGamemode)
        {
            case GamemodeType.Highscore:
                leaderboardName = "Scores_Highscore";
                break;
            case GamemodeType.Easy:
                leaderboardName = "Scores_Easy";
                break;
            case GamemodeType.Hardcore:
                leaderboardName = "Scores_Hardcore";
                break;
        }

        Task<QuerySnapshot> scores = FirebaseManagement.firestoreReference.Collection(leaderboardName).OrderByDescending("score").Limit(10).GetSnapshotAsync();
        await scores;
        if (!scores.IsCompletedSuccessfully)
            return;

        foreach (DocumentSnapshot score in scores.Result.Documents)
        {
            Dictionary<string, object> scoreData = score.ToDictionary();
            GameObject playerItem = Instantiate(playerPrefab, playerListContent);
            playerItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scoreData["name"].ToString();
            playerItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoreData["score"].ToString();
            playerItem.SetActive(true);
        }
    }

    public async void UpdateUserScore()
    {
        try
        {
            if (MainUI.Instance.score < PlayerData.highscore)
                return;

            string leaderboardName = "";
            switch (ObstacleSpawner.currentGamemode)
            {
                case GamemodeType.Highscore:
                    leaderboardName = "Scores_Highscore";
                    break;
                case GamemodeType.Easy:
                    leaderboardName = "Scores_Easy";
                    break;
                case GamemodeType.Hardcore:
                    leaderboardName = "Scores_Hardcore";
                    break;
            }

            Dictionary<string, object> scoreData = new Dictionary<string, object>();
            scoreData.Add("id", PlayerData.playerID);
            scoreData.Add("name", PlayerData.playerName);
            scoreData.Add("score", Mathf.FloorToInt(MainUI.Instance.score));

            Task<QuerySnapshot> doesScoreExist = FirebaseManagement.firestoreReference.Collection(leaderboardName).WhereEqualTo("id", PlayerData.playerID).GetSnapshotAsync();
            await doesScoreExist;

            if (doesScoreExist.Result.Count != 0)
            {
                foreach (DocumentSnapshot snapshot in doesScoreExist.Result.Documents)
                {
                    Task updateTask = snapshot.Reference.UpdateAsync(scoreData);
                    await updateTask;
                    break;
                }
            }
            else
            {
                Task<DocumentReference> addition = FirebaseManagement.firestoreReference.Collection(leaderboardName).AddAsync(scoreData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error updating player score: " + e.Message);
        }
    }

    public void OpenLeaderboard()
    {
        open = true;
        ReloadLeaderboard();
    }

    public void CloseLeaderboard()
    {
        open = false;
    }
}
