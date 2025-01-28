using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")] 
    [SerializeField] private GameObject _hud;
    [SerializeField] private TMP_Text _movesText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _failScreen;
    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _targetObjects;

    [SerializeField] private Button _winButton;
    [SerializeField] private Button _loseButton;
    [SerializeField] private Button _nextLevelButton;
    
    [SerializeField] private StatisticsUI _statisticsUI;

    private void Start()
    {
        HideHUD();
        HideFailScreen();
        HideWinScreen();
        ShowLoginScreen();
        
        _winButton.onClick.AddListener(TowerOfLondonController.Instance.LoadLevel);
        _loseButton.onClick.AddListener(TowerOfLondonController.Instance.LoadLevel);
        _nextLevelButton.onClick.AddListener(TowerOfLondonController.Instance.LoadNextLevel);
    }

    private void OnDestroy()
    {
        _winButton.onClick.RemoveAllListeners();
        _loseButton.onClick.RemoveAllListeners();
        _nextLevelButton.onClick.RemoveAllListeners();
    }

    public void UpdateMoves(int moves)
    {
        if (_movesText != null) {
            _movesText.text = $"Moves Left: {moves}";
        }
    }

    public void UpdateLevel(int level)
    {
        if (_levelText != null) {
            _levelText.text = $"Level: {level}";
        }
    }

    public void ShowWinScreen()
    {
        if (_winScreen && !_winScreen.activeSelf) {
            _winScreen.SetActive(true);
            _statisticsUI.PrintScores();
        }
    }

    public void ShowFailScreen()
    {
        if (_failScreen && !_failScreen.activeSelf) {
            _failScreen.SetActive(true);
        }
    }

    public void HideWinScreen()
    {
        if (_winScreen && _winScreen.activeSelf) {       
            _winScreen.SetActive(false);
        }
    }

    public void HideFailScreen()
    {
        if (_failScreen && _failScreen.activeSelf) {
            _failScreen.SetActive(false);
        }
    }
    
    public void ShowLoginScreen()
    {
        if (_loginScreen && !_loginScreen.activeSelf) {
            _loginScreen.SetActive(true);
        }
    }

    public void HideLoginScreen()
    {
        if (_loginScreen && _loginScreen.activeSelf) {
            _loginScreen.SetActive(false);
        }
    }
    
    public void ShowHUD()
    {
        if (_hud && !_hud.activeSelf) {
            _hud.SetActive(true);
        }
    }

    public void HideHUD()
    {
        if (_hud && _hud.activeSelf) {
            _hud.SetActive(false);
        }
    }
    
    public void ShowTargetObjects() => _targetObjects.SetActive(true);
    public void HideTargetObjects() => _targetObjects.SetActive(false);
}