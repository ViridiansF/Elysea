using UnityEngine;

public class movEnnemi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform target;
    public float speed = 2f;

    public float rotationSpeed = 0.0025f;
    private Rigidbody2D rb;

    private float knockbackTimer = 0f;
    [SerializeField] private float knockbackDuration = 0.15f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Rotate to face the target
        RotateTowardsTarget();

    }

    void FixedUpdate()
    {
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            return; // on ne bouge pas vers la cible pendant le knockback
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }


    void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed);
    }

    public void ApplyKnockback(Vector2 force)
    {
        knockbackTimer = knockbackDuration;

        rb.linearVelocity = Vector2.zero; // optionnel mais souvent mieux
        rb.AddForce(force, ForceMode2D.Impulse);
    }

}
