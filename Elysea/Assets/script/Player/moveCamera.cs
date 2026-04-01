using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    [SerializeField] private Vector3 offset;
    [SerializeField]
    [HideInInspector]
    public Transform target;

    //The speed of the movement
    public float cameraSpeed = 0.1f;


    private Vector3 vel = Vector3.zero;

    void Start()
    {
        if (target == null)
        {
            GameObject playerBoat = GameObject.Find("PlayerBoat");
            if (playerBoat != null)
                target = playerBoat.transform;
            else
                Debug.LogWarning("CameraScript: PlayerBoat non trouvé");
        }
    }

    void FixedUpdate()
    {

        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, cameraSpeed);
        //transform.position = targetPosition;

        
        



    }

}