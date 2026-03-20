using UnityEngine;
using System.Collections.Generic;

public class colliderGoodRange : MonoBehaviour
{

    private List<GameObject> enemiesInRange = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesInRange.Count > 0) 
        {
            Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXX: " + enemiesInRange.Count);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Objet touché : " + collision.gameObject.name + " | Tag détecté : " + collision.gameObject.tag);
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            enemiesInRange.Add(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Objet touché : " + collision.gameObject.name + " | Tag détecté : " + collision.gameObject.tag);
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
