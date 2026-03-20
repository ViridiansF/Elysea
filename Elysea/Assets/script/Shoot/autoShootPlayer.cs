using UnityEngine;

public class autoShootPlayer : shootBullet
{

    colliderGoodRange zone;
    float rotationSpeed = 360f; // Degrees per second






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zone = GetComponent<colliderGoodRange>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("fireTimer : " + fireTimer+" fireRate : " + fireRate);
        if(zone.target != null && fireTimer <= 0f)
        {
            HandleGunRotation();
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
        float angle = Mathf.Atan2(zone.target.position.y - transform.position.y, zone.target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // 2. On tourne progressivement vers cette cible
        // Time.deltaTime permet de rendre le mouvement indépendant des FPS
        anchoring.transform.rotation = Quaternion.Slerp(
            anchoring.transform.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
    }


}
