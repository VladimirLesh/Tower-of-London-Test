using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TowerOfLondonController : MonoBehaviour
{
    public static TowerOfLondonController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TowerSetup towerSetup;
    [SerializeField] private LevelConfig[] levels;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundController _soundController;

    [Header("Settings")]
    [SerializeField] private float ringMoveSpeed = 5f;

    private int currentLevel;
    private int movesLeft;
    private bool isInteractable;
    private float _startTime;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

    private void Start()
    {
        uiManager.ShowLoginScreen();
        uiManager.HideTargetObjects();
    }

    public void LoadLevel()
    {
        uiManager.HideWinScreen();
        uiManager.HideFailScreen();
        uiManager.HideLoginScreen();
        uiManager.ShowHUD();
        uiManager.ShowTargetObjects();
        loadLevel(currentLevel);
        
        _soundController.SetPitch(1f);
    }
    
    public void LoadNextLevel()
    {
        uiManager.HideWinScreen();
        uiManager.HideFailScreen();
        uiManager.HideLoginScreen();
        uiManager.ShowHUD();
        uiManager.ShowTargetObjects();
        loadLevel(currentLevel + 1);
        
        _soundController.SetPitch(1f);
    }

    private void loadLevel(int levelIndex)
    {
        print(levelIndex);
        if (levelIndex < 0 || levelIndex >= levels.Length) {
            return;
        }

        currentLevel = levelIndex;
        LevelConfig config = levels[levelIndex];
        towerSetup.InitializeLevel(config);
        movesLeft = config.maxMoves;
        UpdateUI();
        isInteractable = true;
    }

    public void OnRingSelected(Ring selectedRing, Peg targetPeg)
    {
        if (!isInteractable) return;

        StartCoroutine(RingMovementProcess(selectedRing, targetPeg));
    }

    private IEnumerator RingMovementProcess(Ring ring, Peg targetPeg)
    {
        isInteractable = false;

        if (ValidateMove(ring, targetPeg))
        {
            yield return PerformMoveAnimation(ring, targetPeg);
            movesLeft--;
            UpdateUI();
            CheckWinCondition();
        }

        isInteractable = true;
    }
    
    private bool ValidateMove(Ring selectedRing, Peg targetPeg)
    {
        if (!selectedRing.IsTopRing()) {
            Debug.Log("Cannot move: Selected ring is not the top ring on its peg.");
            return false;
        }

        return true;
    }

    private IEnumerator PerformMoveAnimation(Ring ring, Peg targetPeg)
    {
        _soundController.PlayMove();
        
        Peg currentPeg = ring.CurrentPeg;
        currentPeg.RemoveTopRing();
        Vector3 startPosition = ring.transform.position;
        Vector3 targetPosition = targetPeg.GetSpawnPointPosition() + Vector3.up * ((targetPeg.RingCount + 1) * ring.Height);

        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * ringMoveSpeed;
            ring.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }

        targetPeg.AddRing(ring);
        ring.SetCurrentPeg(targetPeg);
    }

    private void CheckWinCondition()
    {
        if (CheckWinState(levels[currentLevel].targetSequences)) {
            ShowWinScreen();
        }
        else if (movesLeft <= 0) {
            ShowFailScreen();
        }
    }

    private bool CheckWinState(IntListWrapper[] targetSequences)
    {
        for (int i = 0; i < targetSequences.Length; i++)
        {
            Peg peg = towerSetup.GetPeg(i);
            List<int> currentSequence = peg.GetRingSequence();
            
            if (!currentSequence.SequenceEqual(targetSequences[i].sequence)) {
                return false;
            }
        }
        return true;
    }
    
    
    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateMoves(movesLeft);
            uiManager.UpdateLevel(currentLevel + 1);
        }
    }

    private void ShowWinScreen()
    {
        if (uiManager != null)
        {
            SaveGameResult();
            uiManager.HideHUD();
            uiManager.HideTargetObjects();
            uiManager.ShowWinScreen();
        }
        isInteractable = false;
    }

    private void ShowFailScreen()
    {
        if (uiManager != null)
        {
            uiManager.HideHUD();
            uiManager.HideTargetObjects();
            uiManager.ShowFailScreen();
            _soundController.SetPitch(0.5f);
        }
        isInteractable = false;
    }
    
    private void SaveGameResult()
    {
        string playerName = PlayerPrefs.GetString("Username", "");
        float time = Time.time - _startTime;

        GameStatistics.Instance.SaveGameResult(playerName, currentLevel, levels[currentLevel].maxMoves - movesLeft, time);
    }

    public int GetCurrentLevel() => currentLevel;

}