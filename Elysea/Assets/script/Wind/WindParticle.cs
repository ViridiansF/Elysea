using UnityEngine;

public class WindParticle : MonoBehaviour
{
    public Wind wind;                 // ton script Vent (windDirection, windStrength)
    public ParticleSystem ps;
    public float particleSpeedFactor = 0.6f; // vitesse particules = windStrength * factor
    public float smooth = 8f;                // lissage (optionnel)
    private Vector2 currentVel;              // pour lisser

    void Awake()
    {
        if (ps == null) ps = GetComponentInChildren<ParticleSystem>();
        if (wind == null) wind = GetComponent<Wind>();

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
        if (ps == null || wind == null) return;

        Vector2 dir = wind.windDirection.normalized;
        Vector2 targetVel = dir * (wind.windStrength * particleSpeedFactor);

        // lissage pour éviter que ça tremble si tu changes le vent
        currentVel = Vector2.Lerp(currentVel, targetVel, smooth * Time.deltaTime);

        var vel = ps.velocityOverLifetime;

        // On met une valeur constante (MinMaxCurve) sur X et Y
        vel.x = new ParticleSystem.MinMaxCurve(currentVel.x);
        vel.y = new ParticleSystem.MinMaxCurve(currentVel.y);
    }
}

