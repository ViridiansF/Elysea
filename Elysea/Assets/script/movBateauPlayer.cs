using UnityEngine;

public class movBateauPlayer : MonoBehaviour
{
    public float turnSpeed = 220f;        // deg/sec
    public float thrust = 12f;            // force moteur
    public float maxSpeed = 6f;
    public float waterDrag = 1.6f;// vitesse d’alignement visuel      // vitesse max
    public float minAlignmentToMove = 0.35f;

    public Rigidbody2D rb;
    private float moveX;
    private float moveY;

    public Vector2 windSum;
    public void AddWind(Vector2 windForce) => windSum += windForce;
    public void RemoveWind(Vector2 windForce) => windSum -= windForce;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;           
        rb.angularDamping = 0f; 
    }


    // Update is called once per frame
    void Update()
    {   
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(moveX, moveY);

        // 🔄 Moteur/rotation seulement si input
        if (inputDir.sqrMagnitude >= 0.01f)
        {
            if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

            float desiredAngle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(rb.rotation, desiredAngle, turnSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);

            Vector2 forward = transform.up;
            float alignment = Vector2.Dot(forward, inputDir);

            if (alignment > minAlignmentToMove)
            {
                float throttle = Mathf.InverseLerp(minAlignmentToMove, 1f, alignment);
                rb.AddForce(forward * (throttle * thrust), ForceMode2D.Force);
            }
        }

        // 🌬️ Vent : TOUJOURS appliqué, même sans input
        rb.AddForce(windSum, ForceMode2D.Force);

        // ⛔ Limite vitesse (moteur + vent)
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        // 🌊 Freinage eau (ralentit aussi l’effet du vent)
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, waterDrag * Time.fixedDeltaTime);
    }

}
