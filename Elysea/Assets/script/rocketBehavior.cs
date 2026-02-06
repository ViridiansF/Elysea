using UnityEngine;

public class rocketBehavior : bulletBehavior
{
    void onTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger détecté par : " + gameObject.name);
        Debug.Log("Objet touché : " + other.gameObject.name + " tag=" + other.tag);

        if (other.CompareTag("Enemy"))
        {
            Destroy(transform.root.gameObject);
        }
    }
}