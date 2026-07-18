using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D birdRigidbody;
    [SerializeField] private float jumpForce;

    void Update()
    {
//        if(Input.touchCount > 0)
//        {
//            Touch touchInfo = Input.GetTouch(0);

//            if(touchInfo.phase == TouchPhase.Began)
//            {
//                birdRigidbody.linearVelocityY = jumpForce;
//                Debug.Log("Figner started touching");
//            }

//            if(touchInfo.phase == TouchPhase.Ended)
//            {
//                Debug.Log("Figner was released");
//            }

//            if (touchInfo.phase == TouchPhase.Moved)
//            {
//                Debug.Log("Finger is moving");
//            }

//        }
    }

    public void JumpBird(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Jumping the bird");
            birdRigidbody.linearVelocityY = jumpForce;
        }
        if (ctx.canceled)
        {
            Debug.Log("Button was released");
        }
    }

    public void MoveBird(InputAction.CallbackContext ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //disable input
        //play sound
        //show game over screen
        //register score

        /*
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add score points
        //play sound

        /*
        if (collision.gameObject.CompareTag("ScoreZone"))
        {
            Debug.Log("Score!");
        }*/

    }

}
