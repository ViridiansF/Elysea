using UnityEngine;

public class ColisionPlayer : MonoBehaviour
{
    public int PV = 5;
    private movBateauPlayer boat; // remplace par le nom de TON script parent

    void Awake()
    {
        boat = GetComponentInParent<movBateauPlayer>(); // ou ton script de mouvement
    }

    void Start()
    {
        Debug.Log("ColisionPlayer actif sur : " + gameObject.name);
        Debug.Log("Parent movBateauPlayer trouvé ? " + (boat != null));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée par : " + gameObject.name);
        Debug.Log("Objet touché : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Ennemi"))
        {
            // Gérer la collision avec l'ennemi
            var enemy = collision.collider.GetComponent<colisionEnnemi>();

            if (enemy == null)
            {
                Debug.Log("Touched object: " + collision.collider.name);
                Debug.Log("Touched has parent? " + (collision.collider.transform.parent != null));
                Debug.Log("Touched path root: " + collision.collider.transform.root.name);

                return;
            }
            else
            {
                Debug.Log("Dégâts reçus de l'ennemi : " + enemy.damage);
                PV -= enemy.damage;
                if (PV <= 0)
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

        Vent wind = other.GetComponent<Vent>();
        if (wind != null)
            boat.AddWind(wind.WindForce);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Vent wind = other.GetComponent<Vent>();
        if (wind != null)
            boat.RemoveWind(wind.WindForce);
    }
}

