using UnityEngine;

public class returnPoolChildBoss : MonoBehaviour
{
private shootBossChildBirth shooter;

    // Permet de passer la référence du tireur depuis le shooter
    public void SetShooter(shootBossChildBirth s)
    {
        shooter = s;
    }

    public void Die()
    {
        Debug.Log("Enfant mort, retourne dans le pool");
        // On désactive l'enfant
        gameObject.SetActive(false);

        // On le remet dans le pool du shooter
        if (shooter != null)
        {
            shooter.ReturnBulletToPool(gameObject);
        }
    }
}
