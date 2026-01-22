using UnityEngine;

public class DragonHealthSystem : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHealth;
    public Animator animator; 
    private bool isDead = false;
    public AudioSource hurtSound;
    public AudioSource deathSound;
    public GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if(isDead) return;

        currentHealth -= amount;
        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            deathSound.Play(); 
            Die();
        } else {
            hurtSound.Play();
        }
    }

    void Die()
    {
        Debug.Log("Dragon defeated! You win!");
        if(isDead) return;

        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("death");
        animator.SetBool("dead", true);
        isDead = true;
        gameManager.GameVictory();
        Destroy(gameObject, 2f);
    }
}
