using UnityEngine;

public class movEnemyContact : movEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public float speed = 2f;

    public void Init(float newSpeed, Transform newTarget)
    {
        speed = newSpeed;
        target = newTarget;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
        {
            SetTarget();
        }
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


}
