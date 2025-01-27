using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsUI : MonoBehaviour
{
    [SerializeField] private GameObject _scoreViewPrefab;
    [SerializeField] private GameObject _content;

    public void PrintScores()
    {
        List<GameResult> allResults = GameStatistics.Instance.GetAllResults();
        deleteAllChilds();

        foreach (var result in allResults)
        {
            if (result.GetLevelStat(TowerOfLondonController.Instance.GetCurrentLevel()) == null)
                continue;
            
            GameObject scoreView = Instantiate(_scoreViewPrefab, _content.transform);
            ScoreView sv = scoreView.GetComponent<ScoreView>();
            sv.SetName(result.playerName);
            int level = TowerOfLondonController.Instance.GetCurrentLevel();
            sv.SetScore(result.GetLevelStat(level).bestMoves);

            string currentUser = PlayerPrefs.GetString("Username");
            if (result.playerName == currentUser) {
                sv.SetBoldScore();
            }
        }
    }

    private void deleteAllChilds()
    {
        if (_content.transform.childCount > 0)
        {
            for (int i = _content.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = _content.transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }
    }

    private void Update()
    {
        float tempHeight = 0;
        foreach (Transform child in _content.transform)
        {
            tempHeight += child.gameObject.GetComponent<RectTransform>().rect.height;
        }
        
        RectTransform rectTransform = _content.GetComponent<RectTransform>();
        Vector2 delta = new Vector2(rectTransform.sizeDelta.x, 0);
        delta.y += tempHeight;
        _content.GetComponent<RectTransform>().sizeDelta = delta;
    }
}
