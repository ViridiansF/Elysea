using UnityEngine;

public class shootPlayer : MonoBehaviour
{
    private Vector2 mousePos;


    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float fireRate = 0.5f;

    private float fireTimer;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        if(Input.GetMouseButton(0) && fireTimer <= 0f)
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
}
