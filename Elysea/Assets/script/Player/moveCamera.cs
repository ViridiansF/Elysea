using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    [SerializeField] private Vector3 offset;
    [SerializeField] public Transform target;

    //The speed of the movement
    public float cameraSpeed = 0.1f;


    private Vector3 vel = Vector3.zero;
    void FixedUpdate()
    {

        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, cameraSpeed);
        //transform.position = targetPosition;

        
        



    }

}