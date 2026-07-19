using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int currentScore;

    [SerializeField] private GameObject ObstaclePrefab;
    [SerializeField] private float ObstacleSpawnRate;

    [SerializeField] private TextMeshProUGUI scoreDisplayText;

    void Start()
    {
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

}
