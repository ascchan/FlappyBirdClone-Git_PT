using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class APITest : MonoBehaviour
{

    private const string apiUrl = "https://myflappybird-71f1b-default-rtdb.firebaseio.com/";
    private const string allScoresAddress = "scores/.json";

    public string userName;
    public int highestScoreOnDatabase;

    public List<UserLeaderboardEntry> leaderboard = new List<UserLeaderboardEntry>();
    
    [SerializeField] private UIScoreEntry scoreEntryPrefab;
    [SerializeField] private Transform contentParent;

    public void StartDB()
    {
        StartCoroutine( LoadCurrentUserScore() );
        DisplayLeaderboard();
    }

    IEnumerator LoadCurrentUserScore()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + "scores/" + userName + ".json");
        Debug.Log("Loading current user score for: " + userName);

        yield return request.SendWebRequest();
        int.TryParse(request.downloadHandler.text, out highestScoreOnDatabase);
    }

    public void RegisterHighScore(int score)
    {
        if(GameManager.currentScore > highestScoreOnDatabase)
        {
            StartCoroutine( RegisterHighScoreCoroutine(score) );
            DisplayLeaderboard();
        }
    }

    IEnumerator RegisterHighScoreCoroutine(int score)
    {
        UnityWebRequest request = UnityWebRequest.Put( apiUrl + "scores/" + userName + ".json", GameManager.currentScore.ToString() );

        yield return request.SendWebRequest();
        
        Debug.Log("High score registered successfully!" + request.downloadHandler.text );
    }

    public void DisplayLeaderboard()
    {
        StartCoroutine( DownloadLeaderboardCoroutine() );
    }

    IEnumerator DownloadLeaderboardCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + allScoresAddress);

        yield return request.SendWebRequest();

        string cleanText = request.downloadHandler.text;

        cleanText = cleanText.Replace("}", "");
        cleanText = cleanText.Replace("{", "");
        cleanText = cleanText.Replace('"', ' ');
        cleanText = cleanText.Replace(" ", "");
        
        string[] entries = cleanText.Split(',');

        foreach(string entry in entries)
        {
            string[] userAndScore = entry.Split(':');
            if(userAndScore.Length == 2)
            {
                UserLeaderboardEntry newLeaderboardEntry = new UserLeaderboardEntry();
            
                newLeaderboardEntry.leaderboardName = userAndScore[0];
                //newLeaderboardEntry.userScore = int.Parse(userAndScore[1]);
                int.TryParse(userAndScore[1], out newLeaderboardEntry.userScore);

                leaderboard.Add(newLeaderboardEntry);
            }

        }

       leaderboard = leaderboard.OrderByDescending(x => x.userScore).ToList();
       //using System.Linq; is required for the OrderByDescending method to work

       foreach(UserLeaderboardEntry entry in leaderboard)
        {
            UIScoreEntry entryClone = Instantiate(scoreEntryPrefab, contentParent);
            entryClone.userNameText.text = entry.leaderboardName;
            entryClone.scoreText.text = entry.userScore.ToString();
        }
    }

    public void SetUserName(string name)
    {
        userName = name;
        Debug.Log("User name set to: " + userName);
    }

    public string GetUserName()
    {
        return userName;
    }

}

[System.Serializable] 
public class UserLeaderboardEntry
{
    public string leaderboardName;
    public int userScore;
}