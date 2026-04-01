using UnityEngine;

public class AutoShootPlayerRocket : autoShootPlayer
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if(gameObject.name == "Nuklear") {
            bulletPrefab.GetComponent<Bullet_rocketBehavior>().isNuklear = true;
        } else {
            bulletPrefab.GetComponent<Bullet_rocketBehavior>().isNuklear = false;
        }
    }

    // // Update is called once per frame
    // protected void Update()
    // {
        
    // }
}
