using UnityEngine;
using System.Collections;

public class CollisionPlayer : MonoBehaviour
{
    private movBateauPlayer boat; // remplace par le nom de TON script parent
    
    public PlayerHealth playerHealth;

    private float knockbackForce = 2f; 

    private int continuousDamage = 0;

    private float damageTimer = 0f;

    void Awake()
    {
        boat = GetComponentInParent<movBateauPlayer>(); // ou ton script de mouvement
    }

    void Start()
    {

    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0){
            Death();
        }

        if (continuousDamage != 0)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= 1f)
            {
                playerHealth.takeDamage(continuousDamage);
                Debug.Log("Dégâts continus reçus : " + continuousDamage);
                damageTimer = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision détectée par : " + gameObject.name);
        //Debug.Log("Objet touché : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ChildBoss"))
        {
            // Gérer la collision avec l'ennemi
            var enemy = collision.collider.GetComponent<colisionEnemy>();

            if (enemy == null)
            {
                Debug.Log("Touched enemy as no collision : " + collision.collider.name);
                return;
            }
            else
            {
                Debug.Log("Dégâts reçus de l'ennemi : " + enemy.damageContact);
                playerHealth.takeDamage(enemy.damageContact);
            }
            
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Trigger détecté par : " + gameObject.name);
        //Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        if (other.CompareTag("Weather"))
        {
            Wind wind = other.GetComponent<Wind>();
            if (wind != null)
                boat.AddWind(wind.WindForce);
        }

        if (other.CompareTag("Bullet Enemy"))
        {
            //Debug.Log("Dégâts reçus de l'ennemi : " + other.GetComponent<bulletBehavior>().damage);
            playerHealth.takeDamage(other.GetComponent<bulletBehavior>().damage);

            // Knockback 2D
            Rigidbody2D rb = GetComponentInParent<Rigidbody2D>(); // Assure-toi que le joueur a un Rigidbody2D
            if (rb != null)
            {
                // Direction du knockback (du projectile vers le joueur)
                Vector2 knockbackDirection = (Vector2)(transform.position - other.transform.position);
                knockbackDirection.Normalize();
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
            else {
                Debug.LogWarning("Le joueur n'a pas de Rigidbody2D pour le knockback !");}
        }

        if(other.CompareTag("Laser Enemy"))
        {
            playerHealth.takeDamage(other.GetComponent<bulletBehavior>().damage);
            Debug.Log("Dégâts initiaux reçus du laser : " + other.GetComponent<bulletBehavior>().damage);
            continuousDamage = other.GetComponent<bulletBehavior>().damage;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Wind wind = other.GetComponent<Wind>();
        if (wind != null)
            boat.RemoveWind();

        if (other.CompareTag("Laser Enemy") && continuousDamage != 0)
        {
            continuousDamage = 0;
        }
    }

    void Death()
    {
        // Ajouter des effets de mort ici (explosion, son, etc.)
        Time.timeScale = 0f; // Tout s'arrête
        Debug.Log("Game Over");
    }
}

