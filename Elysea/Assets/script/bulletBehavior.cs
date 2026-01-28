using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        rb.linearVelocity = transform.up * speed;
        
    }
}
