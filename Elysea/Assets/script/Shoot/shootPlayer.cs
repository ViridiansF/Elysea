using UnityEngine;

public class shootPlayer : shootBullet
{
    private Vector2 mousePos;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("fireTimer : " + fireTimer+" fireRate : " + fireRate);
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

    protected override void HandleGunRotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        anchoring.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


}
