using UnityEngine;

public class shootEnemy : MonoBehaviour
{
    public GameObject anchoring;

    private Transform target;

    public int damage = 1;
    public int pierce = 1;

    public bool knockback = true;

    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float fireRate = 0.5f;

    private float fireTimer;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponentInParent<movEnemy>().target;

    }

    // Update is called once per frame
    void Update()
    {
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

    private void HandleGunRotation()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        anchoring.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        bulletBehavior bulletScript = bullet.GetComponent<bulletBehavior>();
        bulletScript.damage = damage;
        bulletScript.pierce = pierce;
        bulletScript.knockback = knockback;

    }
}
