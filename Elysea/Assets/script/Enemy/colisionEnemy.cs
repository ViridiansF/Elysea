using UnityEngine;

public class colisionEnemy : MonoBehaviour
{
    public int damageContact = 3;
    public int pv = 3;
    public bool enableContactDeath = true;
    public bool enableKnockbackContact = true;
    public bool enableKnockbackBullet = true;
    private float knockbackForceContact = 5f;
    private float knockbackForceBullet = 5f;


    public AudioSource audioSource;
    public AudioClip enemyDeathSound;

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
        

        if (collision.gameObject.CompareTag("Player"))
        {        


            if (enableContactDeath)
            {
                death();
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
        //Debug.Log("Trigger détecté par : " + gameObject.name);
        //Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        if (other.CompareTag("Bullet") || other.CompareTag("DamageZone"))
        {
            pv -= other.GetComponent<bulletBehavior>().damage;
            Debug.Log("PV ennemi : " + pv);
            if (pv <= 0)
            {
                AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
                Debug.Log("Ennemi détruit");
                death();
            }
            if(other.GetComponent<bulletBehavior>().knockback && enableKnockbackBullet)
            {
                // Appliquer un knockback à l'ennemi
                Rigidbody2D enemyRb = this.transform.root.GetComponent<Rigidbody2D>();
                

                Vector2 dir = (enemyRb.transform.position - transform.position).normalized;
                    Vector2 force = dir * knockbackForceBullet;

                movEnemy enemy = enemyRb.GetComponent<movEnemy>();
                    
                enemy.ApplyKnockback(force);

            }

            other.GetComponent<bulletBehavior>().pierce-=1;
            if (other.GetComponent<bulletBehavior>().pierce < 0)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void death()
    {
        if(transform.root.CompareTag("ChildBoss"))
                {
                    returnPoolChildBoss poolScript = transform.root.GetComponent<returnPoolChildBoss>();
                    poolScript.Die();
                }
                else
                {   
                    Destroy(transform.root.gameObject); // détruit tout l'ennemi
                }
    }

}
