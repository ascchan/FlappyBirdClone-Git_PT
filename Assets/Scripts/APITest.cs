using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APITest : MonoBehaviour
{
    public string websiteUrl;
    public string titleParameter;
    public int pageParameter;

    public ChuckNorrisJoke randomJoke;

    void Start()
    {
        StartCoroutine(LoadWebPage());
    }

    IEnumerator LoadWebPage()
    {
        UnityWebRequest request = UnityWebRequest.Get(websiteUrl + "&s=" + titleParameter + "&page=" + pageParameter.ToString());

        yield return request.SendWebRequest();

        //randomJoke = JsonUtility.FromJson<ChuckNorrisJoke>(request.downloadHandler.text);
        //Debug.Log(randomJoke.value);
        Debug.Log(request.downloadHandler.text);
    }
}

[System.Serializable]
public class ChuckNorrisJoke
{
    public string value;
    public string id;
    public string created_at;
}