using UnityEngine;

public class bullet_RocketBehavior : bulletBehavior
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float delayExplosion = 1f;

    private void OnDestroy()
    {
        // Instancier une explosion à la position du rocket
        GameObject explosion = Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity);
        // Détruire l'explosion après un court délai
        Destroy(explosion, delayExplosion);
    }
}
