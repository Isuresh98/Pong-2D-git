using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSaveBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playCountText;
    public TextMeshProUGUI timeDate;
    public TMP_InputField PlayerName;
    public TextMeshProUGUI PlayerNameDisplay;

    private List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

    private const string ScoreEntriesKey = "ScoreEntries";
    private const int MaxScoreEntries = 30;

    public Leaderboard ledarboard;
    private void Start()
    {
        LoadScoreEntries();
        UpdateScoreText();
    }

    public void AddScoreEntry(int playCount, int score)
    {
        string playerName = PlayerName.text;

        ScoreEntry newEntry = new ScoreEntry(scoreEntries.Count + 1, score, System.DateTime.Now.ToString(), playerName);
        scoreEntries.Add(newEntry);

        // Remove oldest entry if the limit is reached
        if (scoreEntries.Count > MaxScoreEntries)
        {
            scoreEntries.RemoveAt(0);
        }

        SaveScoreEntries();
        UpdateScoreText();
        // Submit the new score to the leaderboard
        //StartCoroutine(ledarboard.SubmitScoreRoutine(score));
    }

    private void UpdateScoreText()
    {
        // Sort the score entries based on score in descending order
        scoreEntries.Sort((x, y) => y.score.CompareTo(x.score));

        string textscore = "";
        string textCount = "";
        string textTimeDate = "";
        string textPlayerName = "";

        for (int i = 0; i < scoreEntries.Count; i++)
        {
            textCount += (i + 1) + "\n";
            textscore += scoreEntries[i].score + "\n";
            textTimeDate += scoreEntries[i].timeDate + "\n";
            textPlayerName += scoreEntries[i].playerName + "\n";
        }

        scoreText.text = textscore;
        playCountText.text = textCount;
        timeDate.text = textTimeDate;
        PlayerNameDisplay.text = textPlayerName;
       
    }

    private void SaveScoreEntries()
    {
        // Remove excess entries if the limit is exceeded
        if (scoreEntries.Count > MaxScoreEntries)
        {
            scoreEntries.RemoveRange(MaxScoreEntries, scoreEntries.Count - MaxScoreEntries);
        }

        string json = JsonUtility.ToJson(new ScoreEntryData(scoreEntries));
        PlayerPrefs.SetString(ScoreEntriesKey, json);
        PlayerPrefs.Save();
    }

    private void LoadScoreEntries()
    {
        if (PlayerPrefs.HasKey(ScoreEntriesKey))
        {
            string json = PlayerPrefs.GetString(ScoreEntriesKey);
            ScoreEntryData data = JsonUtility.FromJson<ScoreEntryData>(json);
            scoreEntries = data.scoreEntries;

            // Remove excess entries if the limit is exceeded
            if (scoreEntries.Count > MaxScoreEntries)
            {
                scoreEntries.RemoveRange(MaxScoreEntries, scoreEntries.Count - MaxScoreEntries);
            }
        }
    }

    [System.Serializable]
    private class ScoreEntry
    {
        public int playCount;
        public int score;
        public string timeDate;
        public string playerName;

        public ScoreEntry(int playCount, int score, string timeDate, string playerName)
        {
            this.playCount = playCount;
            this.score = score;
            this.timeDate = timeDate;
            this.playerName = playerName;
        }
    }

    [System.Serializable]
    private class ScoreEntryData
    {
        public List<ScoreEntry> scoreEntries;

        public ScoreEntryData(List<ScoreEntry> scoreEntries)
        {
            this.scoreEntries = scoreEntries;
        }
    }
}
