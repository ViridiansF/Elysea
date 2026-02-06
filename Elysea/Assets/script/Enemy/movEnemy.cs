using UnityEngine;

public abstract class movEnemy : MonoBehaviour
{
    [HideInInspector] public Transform target = null;

    public float rotationSpeed = 0.0025f;
    protected float knockbackTimer = 0f;
    [SerializeField] private float knockbackDuration = 0.15f;

    protected Rigidbody2D rb;

    public void ApplyKnockback(Vector2 force)
    {
        knockbackTimer = knockbackDuration;

        rb.linearVelocity = Vector2.zero; // optionnel mais souvent mieux
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void RotateTowardsTarget()
    {
        Vector2 direction = target.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed);
    }

    public void SetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log("Cible définie sur le joueur.");
            target = player.transform;
            Debug.Log("Target: " + target);
        }
        else
        {
            Debug.LogWarning("Aucun GameObject avec le tag 'Player' trouvé !");
        }
    }
}

