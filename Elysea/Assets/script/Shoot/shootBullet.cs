using UnityEngine;

public abstract class shootBullet : shoot
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public int damage = 1;
    [SerializeField] public int pierce = 1;
    [SerializeField] public bool knockback = true;
    [Range(0.1f, 10f)]
    [SerializeField] public float speedBullet = 5f;
    [Range(0.1f, 10f)]
    [SerializeField] public float lifeTime = 5f;
    [SerializeField] public float shiftSummoning = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void shoot()
    {
        // Direction du tir (local up = Y pour un sprite 2D)
        Vector3 direction = firingPoint.up; // ou firingPoint.right si ton sprite est orienté horizontalement

        // Calcul de la position de spawn avec décalage
        Vector3 spawnPosition = firingPoint.position + direction * shiftSummoning;

        // Instanciation de la balle
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, firingPoint.rotation);


        bulletBehavior bulletScript = bullet.GetComponent<bulletBehavior>();
        bulletScript.damage = damage;
        bulletScript.pierce = pierce;
        bulletScript.knockback = knockback;
        bulletScript.speed = speedBullet;
        bulletScript.lifeTime = lifeTime;

    }
}
