using UnityEngine;

public class shootEnemy : shoot
{

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
            shootBullet();
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
}
