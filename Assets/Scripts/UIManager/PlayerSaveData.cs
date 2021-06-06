using UnityEngine;


public class PlayerSaveData : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent<int> onUpdateScore;

    public static PlayerSaveData Instance;
    public string playerName;

    public int maxScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDataPersistence();

        onUpdateScore.AddListener(UpdateMaxScore); //from here to MainManager
    }

    private void UpdateMaxScore(int newMaxScore)
    {
        if (maxScore < newMaxScore)
        {
            maxScore = newMaxScore;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public string name;
        public int maxScore;
    }

    internal void SaveDataPersistence()
    {
        SaveData data = new SaveData();
        data.name = playerName;
        data.maxScore = maxScore; //add
        
        string json = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
    }
    internal void LoadDataPersistence()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.name;
            maxScore = data.maxScore; //add
        }
    }
}