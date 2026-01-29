using UnityEngine;

public class ParticulesVent : MonoBehaviour
{
    public Vent vent;                 // ton script Vent (windDirection, windStrength)
    public ParticleSystem ps;

    public float particleSpeedFactor = 0.6f; // vitesse particules = windStrength * factor
    public float smooth = 8f;                // lissage (optionnel)

    private Vector2 currentVel;              // pour lisser

    void Awake()
    {
        if (ps == null) ps = GetComponentInChildren<ParticleSystem>();
        if (vent == null) vent = GetComponent<Vent>();

        // Important : on évite d'additionner des vitesses
        var main = ps.main;
        main.startSpeed = 0f;

        // On active et configure le module
        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.World;
    }

    void LateUpdate()
    {
        if (ps == null || vent == null) return;

        Vector2 dir = vent.windDirection.normalized;
        Vector2 targetVel = dir * (vent.windStrength * particleSpeedFactor);

        // lissage pour éviter que ça tremble si tu changes le vent
        currentVel = Vector2.Lerp(currentVel, targetVel, smooth * Time.deltaTime);

        var vel = ps.velocityOverLifetime;

        // On met une valeur constante (MinMaxCurve) sur X et Y
        vel.x = new ParticleSystem.MinMaxCurve(currentVel.x);
        vel.y = new ParticleSystem.MinMaxCurve(currentVel.y);
    }
}

