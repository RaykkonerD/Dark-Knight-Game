using UnityEngine;

public class Phase2Dragon : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    public Animator animator;
    public AudioSource fireballSound;
    public AudioSource wingFlapSound;
    private bool wingSoundPlaying = false;

    public float timer = 3f;
    public float speed = 1.5f;
    public float attackDistance = 5f;

    private float countDown;
    private bool hasAttacked = false;
    private bool isGoingForward = true;
    private float moveSpeed;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public GameObject fireballPrefab;
    public Transform firePoint;

    void Start()
    {
        countDown = timer;
        moveSpeed = speed;
    }

    void Update()
    {
        initialPosition = new Vector3(
            cameraTransform.position.x - 11f,
            0,
            transform.position.z
        );
        targetPosition = new Vector3(
            cameraTransform.position.x - 2.5f,
            playerTransform.position.y + 1.3f,
            transform.position.z
        );

        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) < 9.5f)
        {
            if(!wingSoundPlaying)
            {
                wingFlapSound.Play();
                wingSoundPlaying = true;
            }
            moveSpeed = speed + 1f;
        } else {
            wingFlapSound.Stop();
            wingSoundPlaying = false;
            moveSpeed = speed;
        }

        if(isGoingForward)
        {
            MoveTowardsTarget();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    void GoBackToInitialPosition()
    {
        isGoingForward = false;
    }

    private void ReturnToInitialPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            initialPosition,    
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, initialPosition) <= 0.1f)
        {
            isGoingForward = true;
            hasAttacked = false;
            countDown = timer;
        }
    }

    private void MoveTowardsTarget()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            transform.position = initialPosition;
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, targetPosition);
        bool isPlayerAtSameHeight = transform.position.y - playerTransform.position.y <= 1.35f;

        if (distance <= attackDistance && isPlayerAtSameHeight && !hasAttacked)
        {
            hasAttacked = true;
            animator.SetTrigger("attack");
            Invoke("GoBackToInitialPosition", 0.5f);
        }
    }

    public void DispararBolaDeFogo()
    {
        fireballSound.Play();
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
