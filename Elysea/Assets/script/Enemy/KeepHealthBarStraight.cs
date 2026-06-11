using UnityEngine;

public class KeepHealthBarStraight : MonoBehaviour
{
    [SerializeField] private Vector3 localOffset = new Vector3(0, 2f, 0); // Ajuste la hauteur ici
    private Quaternion initialRotation;
    private Transform parentTransform;

    void Start()
    {
        // On sauvegarde la rotation de départ
        initialRotation = transform.rotation;
        
        // On récupère le Transform du boss (le parent)
        parentTransform = transform.parent;
    }

    void LateUpdate()
    {
        if (parentTransform != null)
        {
            // 1. On force la position à rester juste au-dessus du centre du boss
            // (En ignorant la rotation du parent grâce à une addition de vecteurs de base)
            transform.position = parentTransform.position + localOffset;
            
            // 2. On force la barre à rester droite face à l'écran
            transform.rotation = initialRotation;
        }
    }
}