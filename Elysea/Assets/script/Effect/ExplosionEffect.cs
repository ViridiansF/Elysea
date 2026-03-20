using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] public float radius = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.one*radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
