using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public Transform drone;
    private Vector3 lastPosition;
    private Vector3 posDiff;
    public bool isCustomOffset;
    public Vector3 offset;
    private Camera cam;
    public float smoothSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
       cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam.fieldOfView = 50f + (rb.velocity.magnitude);
        transform.position = drone.position - (Vector3.ProjectOnPlane(drone.forward, Vector3.up).normalized * 6) + Vector3.up;
        
        

        
        transform.LookAt(drone);
    }
}
