using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;

    public Transform cameraTransform;
    public float speedParallax;
    
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float rePos = cameraTransform.position.x * (1 - speedParallax);
        float distance = cameraTransform.position.x * speedParallax;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (rePos > startPos + length / 2)
        {
            startPos += length;
        }
        else if (rePos < startPos - length / 2)
        {
            startPos -= length;
        }
        
    }
}
