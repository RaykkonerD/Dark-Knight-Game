using UnityEngine;

public class SlideCamera : MonoBehaviour
{
    public float speed = 1f;
    public Transform playerTransform;
    public HeroKnight playerController;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x >= 52f)
         return;
        if(playerTransform == null || playerController == null || playerController.isDead) return;
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(playerTransform.position.x < transform.position.x - 9f){
            playerController.TakeDamage(5);
        }
    }
}
