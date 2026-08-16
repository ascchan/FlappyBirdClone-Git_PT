using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int currentScore;

    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private float ObstacleSpawnRate;

    [SerializeField] private TextMeshProUGUI scoreDisplayText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private APITest api;
    [SerializeField] private GameObject inputUIPanel;
    [SerializeField] private TMP_InputField nameInputField;

  //  private static bool isFirstStart;
    private static string cachedUserName;

    [SerializeField] private Rigidbody2D birdRigidbody;

    void Start()
    {
       //if (isFirstStart)
        //{
            Debug.Log("First start of the game; showing input panel");
            inputUIPanel.SetActive(true);
            Time.timeScale = 0; // pause the game
      /*  } 
        else
        {
            SetUserNameAndStartGame();
        }*/ 
    }
/*
    void Update()
    {
        scoreDisplayText.text = "Score: " + currentScore.ToString();
    }*/

    public void OnEnterButtonClicked()
    {
        string userName = nameInputField.text.Trim();
        Debug.Log("User entered name: " + userName);
       /* if (string.IsNullOrEmpty(userName))
        {
            return;
        }
        SetUserNameAndStartGame();*/

        if (string.IsNullOrEmpty(userName))
        {
            RestartGame();
        }

        cachedUserName = userName;
        api.SetUserName(userName);
        inputUIPanel.SetActive(false);
        Time.timeScale = 1; // resume the game
        api.StartDB();
        

    }

    public void SetUserNameAndStartGame()
    {
        string userName = nameInputField.text;
        if (!string.IsNullOrEmpty(userName))
        {
            cachedUserName = userName;
            api.SetUserName(userName);
            inputUIPanel.SetActive(false);
            Time.timeScale = 1; // resume the game
            //isFirstStart = false;
            api.StartDB();
//            StartGame();
        }

    }

    public void StartGame()
    {
        birdRigidbody.constraints = RigidbodyConstraints2D.None;
        
        InvokeRepeating("SpawnPipes", 1f, ObstacleSpawnRate);
    }

    void SpawnPipes()
    {
        Instantiate(ObstaclePrefab);
    }

    private void Update()
    {
        scoreDisplayText.text = api.GetUserName() + "'s Score: " + currentScore.ToString();
    }

    public void EndGame()
    {
        Time.timeScale = 0; // pause the game
        
        gameOverScreen.SetActive(true);
        api.RegisterHighScore(currentScore);
    }   

    public void RestartGame()
    {
     //   Debug.Log("Restarting the game; isFirstStart: " + isFirstStart + "; cachedUserName: " + cachedUserName);
        Time.timeScale = 1; // resume the game
        currentScore = 0;

        //SetUserNameAndStartGame();
        SceneManager.LoadScene(0);

    }

}
