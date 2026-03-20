using UnityEngine;
using System.Collections.Generic;

public class colliderGoodRange : MonoBehaviour
{

    GameObject nearestEnemy = null;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    public Transform target;
    private Vector3 currentPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesInRange.Count > 0) 
        {
            whoIsNearest();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }

    public void whoIsNearest()
    {
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(currentPosition, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null)
        {
            // Debug.Log("Nearest Enemy: " + nearestEnemy.name);
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}
