using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform pontoAtaque;
    public AudioSource somAtaque;
    public float alcanceAtaque = 0.85f;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] inimigosAlcance = Physics2D.OverlapCircleAll(pontoAtaque.position, alcanceAtaque);

            foreach (Collider2D inimigo in inimigosAlcance)
            {
                NPCHealthSystem npcHealth = inimigo.GetComponent<NPCHealthSystem>();
                if (npcHealth != null)
                {
                    npcHealth.TakeDamage(1f);
                }
                
                DragonHealthSystem dragonHealth = inimigo.GetComponent<DragonHealthSystem>();
                if (dragonHealth != null)
                {
                    dragonHealth.TakeDamage(1f);
                }
            }

            somAtaque.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pontoAtaque.position, alcanceAtaque);
    }
}
