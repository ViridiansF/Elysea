using UnityEngine;

public class Bullet_rocketBehavior : bulletBehavior
{

    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] public bool isNuklear = true;
    private float explosionRadius = 5f;
    private float last;

    public AudioClip shootSound;
    public AudioClip explosionSound;

    // protected override void Start()
    // {
    //     base.Start();
    //     if(!isNuklear) {
    //         ExplosionPrefab.transform.localScale = new Vector3(ExplosionPrefab.transform.localScale.x * 0.25, ExplosionPrefab.transform.localScale.y * 0.25, ExplosionPrefab.transform.localScale.z);
    //     }
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("Explode", lifeTime);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        if(!isNuklear) {
            explosionRadius *= 0.25f;
            last = 0.5f;
        } else
        {
            explosionRadius *= 1f;
            last = 3f;
        }
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
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Explode();
        }
    }
    private void Explode()
    {
        // CancelInvoke();
        GameObject explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, 1f);
        explosion.GetComponent<DamageZoneBehavior>().lifeTime = last;
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA : " + explosion.GetComponent<DamageZoneBehavior>().lifeTime);
    }
}
