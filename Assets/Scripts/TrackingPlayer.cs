using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{
    public Transform playerTransform; 
    public HeroKnight playerController;
    public Animator animator;
    public float moveSpeed = 2f;
    public float stopDistance = 0.9f;

    void Update()
    {
        float distanceToPlayer = transform.position.x - playerTransform.position.x;
        bool playerIsNext = Mathf.Abs(distanceToPlayer) <= 5f;

        bool playerIsFalling = playerController.fallHeightRecorded;
        bool playerIsDead = playerController.isDead;

        if (playerTransform != null && 
            playerIsNext && 
            Mathf.Abs(distanceToPlayer) > stopDistance &&
            !playerIsFalling &&
            !playerIsDead)
        {
            Vector3 npcTransform = transform.position;
            npcTransform.x += -Mathf.Sign(distanceToPlayer) * moveSpeed * Time.deltaTime;
            transform.position = npcTransform;

            animator.SetBool("walking", true);
        } else
        {
            animator.SetBool("walking", false);
        }
    }
}
