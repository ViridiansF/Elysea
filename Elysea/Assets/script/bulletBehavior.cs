using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    public int damage = 1;
    public int pierce = 1;
    public bool knockback = true;
    
    [Range(1, 10)]
    [SerializeField] public float speed = 10f;

    [Range(1, 10)]
    [SerializeField] public float lifeTime = 3f;

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
