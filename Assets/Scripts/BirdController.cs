using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D birdRigidbody;
    [SerializeField] private float jumpForce;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touchInfo = Input.GetTouch(0);

            if(touchInfo.phase == TouchPhase.Began)
            {
                birdRigidbody.linearVelocityY = jumpForce;
//                Debug.Log("Figner started touching");
            }

            if(touchInfo.phase == TouchPhase.Ended)
            {
                Debug.Log("Figner was released");
            }

            if (touchInfo.phase == TouchPhase.Moved)
            {
                Debug.Log("Finger is moving");
            }

        }
    }
}
