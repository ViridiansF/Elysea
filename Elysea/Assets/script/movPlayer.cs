using UnityEngine;

public class movPlayer : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    private float moveX;
    private float moveY;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (moveX, moveY).normalized * movementSpeed;
    }
}
