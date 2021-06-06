using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _scoreText;

    [SerializeField] private TMPro.TMP_InputField _nameField;
    [SerializeField] private Button _playGame;
    [SerializeField] private Button _exitGame;
    private MainMenu _mainMenu;

    public void Configure(MainMenu mainMenu)
    {
        _mainMenu = mainMenu;
    }
    private void Awake()
    {
        _nameField.text = PlayerSaveData.Instance.playerName;
        _playGame.onClick.AddListener(()=> _mainMenu.StartGame());
        _exitGame.onClick.AddListener(()=> _mainMenu.CloseGame());
        _nameField.onValueChanged.AddListener(delegate {_mainMenu.SaveNameData(_nameField.text);});

    }

    public void ChangeNameField(string nameValue)
    {
        PlayerSaveData.Instance.playerName = nameValue;
    }

    public void ShowInfo()
    {
        _scoreText.text = $"Name: {PlayerSaveData.Instance.playerName} Score: {PlayerSaveData.Instance.maxScore}";
    }
}
