using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public static GameStatistics Instance { get; private set; }

    private List<GameResult> _allResults = new List<GameResult>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStatistics();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGameResult(string playerName, int level, int moves, float time)
    {
        GameResult userResult = _allResults.Find(result => result.playerName == playerName);

        if (userResult == null)
        {
            userResult = new GameResult(playerName);
            _allResults.Add(userResult);
        }

        userResult.UpdateLevelStat(level, moves, time);

        SaveStatistics();
    }

    private void LoadStatistics()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _allResults = JsonUtility.FromJson<GameResultsWrapper>(json).results;
        }
    }

    private void SaveStatistics()
    {
        GameResultsWrapper wrapper = new GameResultsWrapper { results = _allResults };
        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(GetFilePath(), json);
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "game_score.json");
    }

    public List<GameResult> GetAllResults()
    {
        return _allResults;
    }

    [System.Serializable]
    private class GameResultsWrapper
    {
        public List<GameResult> results;
    }
}