using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform playerTransform;
    public float speed = 2f;

    private Rigidbody2D rb;
    private int direction = -1;

    private bool move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(playerTransform.position.x >= 20f)
        {
            move = true;
        }
        
        if(move == false) return;

        rb.linearVelocity = new Vector2(direction * speed, 0);

        if (direction == 1 && transform.position.x >= rightPoint.position.x)
        {
            direction = -1;
        }
        else if (direction == -1 && transform.position.x <= leftPoint.position.x)
        {
            direction = 1;
        }
    }
}