using UnityEngine;

public class DragonMove : MonoBehaviour
{
    public float speed = 2f;
    public Transform edgeCheck;
    public float checkDistance = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float dir = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

        // Raycast para baixo
        RaycastHit2D hit = Physics2D.Raycast(
            edgeCheck.position,
            Vector2.down,
            checkDistance,
            groundLayer
        );

        // Se não tem chão à frente → vira
        if (!hit)
        {
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignora o player
        if (collision.gameObject.CompareTag("Player"))
            return;

        if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            if (Mathf.Abs(normal.x) > 0.5f)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Visualiza o raycast no editor
    void OnDrawGizmos()
    {
        if (edgeCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                edgeCheck.position,
                edgeCheck.position + Vector3.down * checkDistance
            );
        }
    }
}