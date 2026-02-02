using UnityEngine;

public class collisionEnnemi : MonoBehaviour
{
    public int damageContact = 3;

    public int pv = 3;

    public bool enableContactDeath = true;

    public bool enableKnockbackContact = true;
    public bool enableKnockbackBullet = true;
    private float knockbackForceContact = 5f;
    private float knockbackForceBullet = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger détecté par : " + gameObject.name);
        Debug.Log("Objet touché : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            if (enableContactDeath)
            {
                Destroy(this.transform.root.gameObject); // détruit tout l'ennemi
            }
            else
            {
                // Appliquer un knockback au joueur
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null && enableKnockbackContact)
                {
                    Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    playerRb.AddForce(knockbackDirection * knockbackForceContact, ForceMode2D.Impulse);
                }
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger détecté par : " + gameObject.name);
        Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        if (other.CompareTag("Bullet"))
        {
            pv -= other.GetComponent<bulletBehavior>().damage;
            Debug.Log("PV ennemi : " + pv);
            if (pv <= 0)
            {
                Debug.Log("Ennemi détruit");
                Destroy(transform.root.gameObject); // détruit tout l'ennemi
            }
            if(other.GetComponent<bulletBehavior>().knockback && enableKnockbackBullet)
            {
                // Appliquer un knockback à l'ennemi
                Rigidbody2D enemyRb = this.transform.root.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Debug.Log("Ennemi knockback appliqué bullet");

                    Vector2 dir = (enemyRb.transform.position - transform.position).normalized;
                    Vector2 force = dir * knockbackForceBullet;

                    movEnnemi enemy = enemyRb.GetComponent<movEnnemi>();
                    if (enemy != null)
                        enemy.ApplyKnockback(force);
                    else
                        enemyRb.AddForce(force, ForceMode2D.Impulse);
                }

            }

            other.GetComponent<bulletBehavior>().pierce-=1;
            if (other.GetComponent<bulletBehavior>().pierce < 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
