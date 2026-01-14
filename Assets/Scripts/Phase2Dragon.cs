using UnityEngine;

public class Phase2Dragon : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float timer = 3f;
    public float speed = 4f;
    public float attackDistance = 5f;

    private float countDown;
    private bool atacou = false;

    void Start()
    {
        countDown = timer;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            transform.position = new Vector3(
                cameraTransform.position.x - 11f,
                transform.position.y,
                transform.position.z
            );
            return;
        }

        Vector3 targetPosition = new Vector3(
            cameraTransform.position.x - 2.5f,
            playerTransform.position.y,
            transform.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance <= attackDistance && !atacou)
        {
            atacou = true;
            animator.SetTrigger("attack");
        }
    }
}
