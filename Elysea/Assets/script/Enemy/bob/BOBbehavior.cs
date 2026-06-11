using UnityEngine;

public class BOBbehavior : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public float zoneOK;
    [SerializeField] public float neutralMaxDistanceToReach = 20f;
    float x;
    float y;
    float xr;
    float yr;
    float pasDistanceReach = 20f;
    Vector2 moveGoal;
    [SerializeField] public GameObject pointVisuelPrefab; 
    private GameObject pointInstancie; // Garde une trace du point créé


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveGoal = findMoveGoal(this.transform.position.x, this.transform.position.y);
        pointInstancie = Instantiate(pointVisuelPrefab, moveGoal, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        Debug.Log("dist : " + Vector2.Distance(this.transform.position, moveGoal) + " pos : " + this.transform.position + " goal : " + moveGoal);
        if (Mathf.Abs(Vector2.Distance(this.transform.position, moveGoal)) < zoneOK)
        {
            moveGoal = findMoveGoal(x, y);
            if (pointInstancie != null)
            {
                pointInstancie.transform.position = moveGoal;
            }
        }
        rb.linearVelocity = moveGoal * speed;
    }

    public Vector2 findMoveGoal(float x, float y)
    {
        xr = Random.Range(x - neutralMaxDistanceToReach, x + neutralMaxDistanceToReach);
        yr = Random.Range(y - neutralMaxDistanceToReach, y + neutralMaxDistanceToReach);
        Vector2 directionAleatoire = new Vector2(xr, yr);
        return directionAleatoire;
    }

    void OnDrawGizmos()
    {
        // Si le jeu est lancé et que moveGoal a été défini
        if (Application.isPlaying)
        {
            // 1. Dessine une sphère rouge là où est la cible
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(moveGoal, 0.5f); // 0.5f est le rayon de la sphère

            // 2. Dessine une ligne blanche entre le boss et sa cible
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, moveGoal);
        }
    }
}
