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
//         if(Input.GetKey("w") && !Input.GetKey("s")) {
//             transform.position += transform.TransformDirection(Vector2.up) * Time.deltaTime * movementSpeed;
//         } else if (Input.GetKey("s") && !Input.GetKey("w")) {
//             transform.position += transform.TransformDirection(Vector2.down) * Time.deltaTime * movementSpeed;
// }

//         if(Input.GetKey("a") && !Input.GetKey("d")) {
//             transform.position += transform.TransformDirection(Vector2.left) * Time.deltaTime * movementSpeed;
//         } else if (Input.GetKey ("d") && !Input.GetKey("a")) {
//            transform.position += transform.TransformDirection(Vector2.right) * Time.deltaTime * movementSpeed;
//         }    

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (moveX, moveY).normalized * movementSpeed;
    }
}
