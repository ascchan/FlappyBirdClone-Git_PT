using UnityEngine;

public class PipeObstacle : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private float upRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-upRange, upRange), 0);
        Destroy(gameObject, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * movingSpeed * Time.deltaTime;
    }
}
