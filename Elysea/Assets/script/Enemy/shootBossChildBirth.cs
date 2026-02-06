using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class shootBossChildBirth : shoot
{
    public int damage = 1;
    public int speed = 5;
    public int hp = 3;

    public int maxBullets = 10;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null){
            target = GetComponentInParent<movEnemy>().target;
            Debug.Log("Target: " + target);

            for (int i = 0; i < maxBullets; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }


        HandleGunRotation();
        if(fireTimer <= 0f)
        {
            shoot();

            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    protected override void HandleGunRotation()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        anchoring.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected void shoot()
    {
        if (bulletPool.Count == 0) return;

        GameObject bullet = bulletPool.Dequeue();
        bullet.transform.position = firingPoint.position;
        bullet.transform.rotation = firingPoint.rotation;
        bullet.SetActive(true);

        returnPoolChildBoss bulletScript = bullet.GetComponent<returnPoolChildBoss>();
        if (bulletScript != null)
        {
            bulletScript.SetShooter(this); // "this" = le tireur actuel
        }

        colisionEnemy enemyScript = bullet.GetComponentInChildren<colisionEnemy>();
        movEnemyContact movScript = bullet.GetComponent<movEnemyContact>();

        movScript.speed = speed;
        movScript.target = target;

        enemyScript.damageContact = damage;
        enemyScript.pv = hp;
        enemyScript.enableContactDeath = true;
        enemyScript.enableKnockbackBullet = true;
        enemyScript.enableKnockbackContact = true;

        
    }
    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
    }


}
