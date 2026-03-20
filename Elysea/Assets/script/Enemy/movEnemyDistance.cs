using UnityEngine;

public class movEnemyDistance : movEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 2f;

    public bool enableRetreat = true;

    public float playerDistanceMin = 10f;
    public float retreatDistance = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            Debug.Log("Target is null in movEnemyDistance");
            SetTarget();
        }
        else
        {
            Debug.Log("Target already set in movEnemyDistance");
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

        Vector2 delta = (Vector2)target.position - (Vector2)transform.position;
        float distance = delta.magnitude;

        // Si on est trop proche
        if (distance < playerDistanceMin)
        {
            // Si on est dans la zone de retraite, reculer
            if (enableRetreat)
                delta = -delta; 
            else
                return;
        }
        else if (distance < retreatDistance && distance >= playerDistanceMin)
        {
            return; // Ne rien faire si on est dans la zone de sécurité
        }
        rb.MovePosition(rb.position + delta.normalized * speed * Time.fixedDeltaTime);
    }


    



}
