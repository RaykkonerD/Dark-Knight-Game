using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.NextPhase();
        }
    }
}
