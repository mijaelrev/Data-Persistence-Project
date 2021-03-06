using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public Text MaxScoreText; //add
    [SerializeField] public int maxScore;  //add
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    void Start()
    {
        PlayerSaveData.Instance.LoadDataPersistence();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity); //var to Brick new instance
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint); //Invoked in Brick class
            }
        }
        maxScore = PlayerSaveData.Instance.maxScore;
        UpdateMaxScore(maxScore);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        UpdateMaxScore(m_Points);
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        PlayerSaveData.Instance.SaveDataPersistence();//add
    }

    private void UpdateMaxScore(int newScoreValue) //New Scripts
    {
        if(maxScore < newScoreValue)
        {
            maxScore = newScoreValue;

            PlayerSaveData.Instance.onUpdateScore?.Invoke(maxScore); 
        }

        MaxScoreText.text = $"Best Score :{PlayerSaveData.Instance.playerName} : {PlayerSaveData.Instance.maxScore}";

    }
}
