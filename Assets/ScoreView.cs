using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;

    public void SetName(string value) => _nameText.text = value;
    public void SetScore(int value) => _scoreText.text = value.ToString();

    public void SetBoldScore()
    {
        _scoreText.fontStyle = (FontStyles)FontStyle.Bold;
        _nameText.fontStyle = (FontStyles)FontStyle.Bold;
    }
}
