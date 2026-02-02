using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    private movBateauPlayer boat; // remplace par le nom de TON script parent
    
    public PlayerHealth playerHealth;

    void Awake()
    {
        boat = GetComponentInParent<movBateauPlayer>(); // ou ton script de mouvement
    }

    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée par : " + gameObject.name);
        Debug.Log("Objet touché : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Gérer la collision avec l'ennemi
            var enemy = collision.collider.GetComponent<collisionEnnemi>();

            if (enemy == null)
            {
                Debug.Log("Touched enemy as no collision : " + collision.collider.name);
                return;
            }
            else
            {
                Debug.Log("Dégâts reçus de l'ennemi : " + enemy.damageContact);
                playerHealth.currentHealth -= enemy.damageContact;
                if (playerHealth.currentHealth <= 0)
                {
                    Debug.Log("Joueur détruit");
                    Destroy(transform.root.gameObject); // détruit tout le joueur
                }
            }
            
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger détecté par : " + gameObject.name);
        Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        Wind wind = other.GetComponent<Wind>();
        if (wind != null)
            boat.AddWind(wind.WindForce);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Wind wind = other.GetComponent<Wind>();
        if (wind != null)
            boat.RemoveWind();
    }
}

