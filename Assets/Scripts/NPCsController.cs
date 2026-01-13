using UnityEngine;

public class NPCsController : MonoBehaviour
{
    public Transform playerTransform;
    public HeroKnight playerController;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;
    private float attackTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackTimer = 0f;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        
        float dif = playerTransform.position.x - transform.position.x;
        spriteRenderer.flipX = dif >= 0;
        
        if (PlayerIsNext() && attackTimer <= 0f && !animator.GetBool("attacking") && !animator.GetBool("dead"))
        {
            StartAttack();
        }
        else if (!PlayerIsNext() || attackTimer > 0f)
        {
            animator.SetBool("attacking", false);
        }
    }

    private bool PlayerIsNext()
    {
        float distanceToPlayer = transform.position.x - playerTransform.position.x;
        bool isNext = Mathf.Abs(distanceToPlayer) <= attackRange && Mathf.Abs(transform.position.y - playerTransform.position.y) <= 0.9f * attackRange;
        return isNext;
    }
    
    private void StartAttack()
    {
        animator.SetBool("attacking", true);
        Invoke("DealDamage", 0.3f);
        attackTimer = attackCooldown;
    }

    private void DealDamage()
    {
        if (PlayerIsNext())
        {
            playerController.TakeDamage(1);
        }
    }
}