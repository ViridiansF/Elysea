using UnityEngine;

public class autoShootPlayer : shootBullet
{

    colliderGoodRange zone;
    [SerializeField]  private float rotationSpeed = 360f; // Degrees per second






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zone = GetComponent<colliderGoodRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if(zone.target != null)
        {
            HandleGunRotation();
        //Debug.Log("fireTimer : " + fireTimer+" fireRate : " + fireRate);
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
    }

    protected override void HandleGunRotation()
    {
        Vector3 dir = zone.target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);


        anchoring.transform.rotation = Quaternion.RotateTowards(
            anchoring.transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
    }
}
