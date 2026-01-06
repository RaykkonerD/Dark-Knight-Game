using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float rotateSpeed = 720f;
    public float disappearTime = 0.8f;

    private bool pickedUp = false;
    private SpriteRenderer sprite;
    private Collider2D col;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!pickedUp) 
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
            return;
        }

        // Sobe
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // Gira
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        // Fade out
        Color c = sprite.color;
        c.a -= Time.deltaTime / disappearTime;
        sprite.color = c;

        // Destrói quando invisível
        if (sprite.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (pickedUp) return;
        
        pickedUp = true;
        col.enabled = false;
    }
}
