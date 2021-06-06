using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuOptions _manuOption;
    //[SerializeField] private EnterName _nameEnter;
    private void Awake()
    {
        //_nameEnter.Configure(this);
        _manuOption.Configure(this);
    }
    private void Start()
    {
        _manuOption.ShowInfo();
    }
    internal void StartGame()
    {
        SceneManager.LoadScene(1);
        
    }

    internal void SaveNameData(string nameValue)
    {
        _manuOption.ChangeNameField(nameValue);
        PlayerSaveData.Instance.SaveDataPersistence();
        _manuOption.ShowInfo();
    }
    internal void LoadNameData()
    {

        //_nameField.SetTextWithoutNotify(PlayerSaveData.Instance.name);

        PlayerSaveData.Instance.LoadDataPersistence();
    }
    internal void CloseGame()
    {
        PlayerSaveData.Instance.SaveDataPersistence();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // Goodbye
        #endif
        
    }

}
