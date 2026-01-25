using UnityEngine;
using System.Collections; // Necessário para Coroutines

public class DragonHealthSystem : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public float maxHealth = 10;
    private float currentHealth;

    [Header("Configurações de Invencibilidade")]
    public float invulnerabilityDuration = 1.5f; // Tempo que fica invencível
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer; // Para efeito visual

    [Header("Componentes")]
    public Animator animator; 
    private bool isDead = false;
    public AudioSource hurtSound;
    public AudioSource deathSound;
    public GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pega o renderizador para mudar a cor
    }

    public void TakeDamage(float amount)
    {
        // 1. Se estiver morto OU invencível, ignora o dano
        if(isDead || isInvulnerable) return;

        currentHealth -= amount;
        animator.SetTrigger("hurt");

        // 2. Inicia a rotina de invencibilidade
        StartCoroutine(BecomeInvulnerable());

        if (currentHealth <= 0)
        {
            deathSound.Play(); 
            Die();
        } else {
            hurtSound.Play();
        }
    }

    // A mágica acontece aqui
    IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        
        // Efeito visual: Fica vermelho claro e meio transparente
        if(spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 0.5f);

        // Espera o tempo definido
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Volta ao normal
        if(spriteRenderer != null)
            spriteRenderer.color = Color.white;

        isInvulnerable = false;
    }

    void Die()
    {
        Debug.Log("Dragon defeated! You win!");
        if(isDead) return;

        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("death");
        animator.SetBool("dead", true);
        isDead = true;
        
        // Garante que a cor volta ao normal se morrer enquanto invencível
        if(spriteRenderer != null) spriteRenderer.color = Color.white;

        gameManager.GameVictory();
        Destroy(gameObject, 2f);
    }
}