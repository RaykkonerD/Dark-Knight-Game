using UnityEngine;

public class NPCHealthSystem : MonoBehaviour
{
    public float maxHealth = 3;
    private float currentHealth;
    public Animator animator; 
    private bool isDead = false;
    public AudioSource hurtSound;
    public AudioSource deathSound;

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
        if(isDead) return;

        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("death");
        animator.SetBool("dead", true);
        isDead = true;
        Destroy(gameObject, 2f);
    }
}
