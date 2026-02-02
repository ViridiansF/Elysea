using UnityEngine;

public abstract class shoot : MonoBehaviour
{
    public GameObject anchoring;
    protected Transform target;
    public int damage = 1;
    public int pierce = 1;
    public bool knockback = true;
    public float speedBullet = 5f;
    public float lifeTime = 5f;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float fireRate = 0.5f;
    protected float fireTimer;

    protected abstract void HandleGunRotation();

    protected void shootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        bulletBehavior bulletScript = bullet.GetComponent<bulletBehavior>();
        bulletScript.damage = damage;
        bulletScript.pierce = pierce;
        bulletScript.knockback = knockback;
        bulletScript.speed = speedBullet;
        bulletScript.lifeTime = lifeTime;

    }
}
