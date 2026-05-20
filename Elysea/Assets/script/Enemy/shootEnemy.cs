using UnityEngine;

public class shootEnemy : shootBullet
{
    [SerializeField] public float rotationSpeed = 5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomValue = Random.Range(-1.0f, 1.0f); // entre 0 et 10
        fireTimer = fireRate+randomValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            target = GetComponentInParent<movEnemy>().target;
            return;
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
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Récupère l'angle actuel
        float currentAngle = anchoring.transform.eulerAngles.z;

        // Interpolation vers l'angle cible avec un facteur de vitesse
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Applique la rotation
        anchoring.transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
}
