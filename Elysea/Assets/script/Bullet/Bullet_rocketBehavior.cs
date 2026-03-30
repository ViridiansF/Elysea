using UnityEngine;

public class Bullet_rocketBehavior : bulletBehavior
{

    [SerializeField] private GameObject ExplosionPrefab;

    private bool isQuitting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Explode", lifeTime);
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // On détecte si l'utilisateur quitte le jeu
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }
    private void Explode()
    {
        // CancelInvoke();
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
