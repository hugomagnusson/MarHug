using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform drone;
    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = drone.position-(Vector3.ProjectOnPlane(drone.forward,Vector3.up)*3)+Vector3.up;
        

        
        transform.LookAt(drone);
    }
}
