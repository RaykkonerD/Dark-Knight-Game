using UnityEngine;

public class Phase3Dragon : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    public Animator animator;
    public AudioSource fireballSound;
    public AudioSource wingFlapSound;

    [Header("Movement & Timing")]
    public float timer = 0.2f;              // tempo de espera antes de começar a se mover
    public float speed = 1.5f;            // velocidade normal
    public float fastSpeedMultiplier = 2.5f; // quanto mais rápido quando está perto
    public float attackDistance = 5f;

    [Header("Spawn Positions")]
    public float leftOffset = -11f;       // quão longe à esquerda da câmera
    public float rightOffset = 11f;       // quão longe à direita da câmera
    public float topOffset = 11f;         // quão acima da câmera
    public float sideYVariation = 4f;     // variação de altura nos lados

    private float countDown;
    private bool hasAttacked = false;
    private bool isGoingForward = true;
    private float currentMoveSpeed;

    private Vector3 spawnPosition;
    private Vector3 attackTargetPosition;

    public GameObject fireballPrefab;
    public Transform firePoint;

    private bool wingSoundPlaying = false;

    void Start()
    {
        countDown = timer;
        currentMoveSpeed = speed;
        ChooseRandomSpawnPosition();           
    }

    void Update()
    {
        // Atualiza a posição-alvo de ataque (onde o dragão vai tentar atacar o player)
        attackTargetPosition = new Vector3(
            cameraTransform.position.x - 2.5f,      // um pouco à frente da câmera
            playerTransform.position.y + 1.3f,
            transform.position.z
        );

        // Controla som de asas
        float distToCamera = Mathf.Abs(cameraTransform.position.x - transform.position.x);
        if (distToCamera < 10f)
        {
            if (!wingSoundPlaying)
            {
                wingFlapSound.Play();
                wingSoundPlaying = true;
            }
            currentMoveSpeed = speed * fastSpeedMultiplier;
        }
        else
        {
            wingFlapSound.Stop();
            wingSoundPlaying = false;
            currentMoveSpeed = speed;
        }

        if (isGoingForward)
        {
            MoveTowardsTarget();
        }
        else
        {
            ReturnToSpawnPosition();
        }
    }

    private void ChooseRandomSpawnPosition()
    {
        int side = Random.Range(0, 3); // 0 = esquerda, 1 = direita, 2 = cima

        Vector3 basePos = cameraTransform.position;

        switch (side)
        {
            case 0: // Esquerda
                spawnPosition = new Vector3(
                    basePos.x + leftOffset,
                    basePos.y + Random.Range(-sideYVariation, sideYVariation),
                    transform.position.z
                );
                break;

            case 1: // Direita
                spawnPosition = new Vector3(
                    basePos.x + rightOffset,
                    basePos.y + Random.Range(-sideYVariation, sideYVariation),
                    transform.position.z
                );
                break;

            case 2: // Cima
                spawnPosition = new Vector3(
                    basePos.x + Random.Range(-8f, 8f), // um pouco de variação horizontal
                    basePos.y + topOffset,
                    transform.position.z
                );
                break;
        }

        // Coloca o dragão na posição de spawn imediatamente
        transform.position = spawnPosition;

        // Garante que ele já nasça olhando para o lado certo
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        // Verifica a posição X relativa entre Player e Dragão
        if (playerTransform.position.x > transform.position.x)
        {
            // Player está à direita -> Rotação normal (0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Player está à esquerda -> Gira 180 graus no eixo Y (espelha horizontalmente)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ReturnToSpawnPosition()
    {
        // Enquanto volta, você pode decidir se quer que ele olhe pro spawn ou continue olhando pro player.
        // Se quiser que ele volte "de costas" olhando pro player, deixe sem nada.
        // Se quiser que ele vire para onde está indo, descomente a linha abaixo:
        // LookAtTarget(spawnPosition); 

        transform.position = Vector3.MoveTowards(
            transform.position,
            spawnPosition,
            currentMoveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, spawnPosition) <= 0.2f)
        {
            isGoingForward = true;
            hasAttacked = false;
            countDown = timer;
            ChooseRandomSpawnPosition(); // ← nova posição aleatória!
        }
    }

    private void MoveTowardsTarget()
    {
        // Faz o dragão SEMPRE olhar para o player nesta fase (espera ou ataque)
        LookAtPlayer();

        // Espera o countdown antes de começar a se mover
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            attackTargetPosition,
            currentMoveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, attackTargetPosition);
        bool isCloseInHeight = Mathf.Abs(transform.position.y - playerTransform.position.y) <= 1.4f;

        if (distance <= attackDistance && isCloseInHeight && !hasAttacked)
        {
            hasAttacked = true;
            animator.SetTrigger("attack");
            Invoke(nameof(GoBack), 0.4f); // tempo pra animação de ataque
        }
    }

    private void GoBack()
    {
        isGoingForward = false;
    }

    public void DispararBolaDeFogo()
    {
        fireballSound.Play();
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}