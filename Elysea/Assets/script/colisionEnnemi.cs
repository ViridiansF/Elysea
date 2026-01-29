using UnityEngine;

public class colisionEnnemi : MonoBehaviour
{
    public int damage = 1;

    public int pv = 3;
    
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
            // Handle collision with player
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger détecté par : " + gameObject.name);
        Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        if (other.CompareTag("Bullet"))
        {
            pv -= damage;
            Debug.Log("PV ennemi : " + pv);
            if (pv <= 0)
            {
                Debug.Log("Ennemi détruit");
                Destroy(transform.root.gameObject); // détruit tout l'ennemi
            }
            
        }
    }
}
