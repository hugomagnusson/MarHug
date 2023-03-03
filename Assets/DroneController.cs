using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
   public float uprightTorque = 0;
   private Rigidbody rb;
   private float upThrust;

   public float thrust; 
   public float rotFactor;
   private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
         upThrust = 1;
         rotation = new Vector3(0,0,0);
         if (Input.GetKey ("w"))
         rotation.x = 1;
         if (Input.GetKey ("s"))
         rotation.x = -1;
         if (Input.GetKey ("q"))
         rotation.y = 1;
         if (Input.GetKey ("e"))
         rotation.y = -1;
         if (Input.GetKey ("a"))
         rotation.z = 1;
         if (Input.GetKey ("d"))
         rotation.z = -1;
         if (Input.GetKey ("left shift"))
         upThrust = 1+thrust;
         if (Input.GetKey ("left ctrl"))
         upThrust = 1-thrust;
        
      
    }
    void FixedUpdate(){
      rb.AddRelativeTorque(rotation*rotFactor);
      var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
      rb.AddTorque(new Vector3(rot.x, rot.y, rot.z)*uprightTorque);
      rb.AddRelativeForce(upThrust*Physics.gravity*(-1f));
    }
}
