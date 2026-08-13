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

    [SerializeField] private Rigidbody2D birdRigidbody;

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
        scoreDisplayText.text = "Score: " + currentScore.ToString();
    }

    public void EndGame()
    {
        Time.timeScale = 0; // pause the game
        
        gameOverScreen.SetActive(true);
        api.RegisterHighScore(currentScore);
    }   

    public void RestartGame()
    {
        Time.timeScale = 1; // resume the game
        currentScore = 0;

        SceneManager.LoadScene(0);

    }

}
