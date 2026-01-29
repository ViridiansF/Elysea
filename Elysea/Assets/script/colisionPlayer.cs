using UnityEngine;

public class ColisionPlayer : MonoBehaviour
{
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

