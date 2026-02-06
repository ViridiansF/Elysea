using UnityEngine;

public abstract class shoot : MonoBehaviour
{
    public GameObject anchoring;
    protected Transform target;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float fireRate = 0.5f;
    protected float fireTimer;
    

    protected abstract void HandleGunRotation();


}
