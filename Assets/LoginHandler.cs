using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _playButton;
    
    

    private void Start()
    {
        var name = PlayerPrefs.GetString("Username", "");
        _inputField.text = name;
        _playButton.onClick.AddListener(checkCanPlay);
    }

    private bool CheckInputString()
    {
        if (_inputField.text.Length == 0)
        {
            print("Please enter a valid username");
            return false;
        }

        return true;
    }

    private void checkCanPlay()
    {
        if (CheckInputString())
        {
            PlayerPrefs.SetString("Username", _inputField.text);
            TowerOfLondonController.Instance.LoadLevel();
        }
    }
}
