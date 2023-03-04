using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
   public float uprightTorque = 0;
    public float angleTorque = 0;
   private Rigidbody rb;
   private float upThrust;
    public float sideThrust;
    public Transform missileAnchor;

   public float thrust = 0.5f; 
   public float rotFactor;
   private Vector3 rotation;
   public Object ball;
    private GameObject tempBall;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //thrust = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
         upThrust = 0.9f;
         rotation = new Vector3(0,0,0);
         if (Input.GetKey ("w"))
         rotation.x = 1;
         if (Input.GetKey ("s"))
         rotation.x = -1;
         if (Input.GetKey ("e"))
         rotation.y = 0.7f;
         if (Input.GetKey ("q"))
         rotation.y = -0.7f;
        if (Input.GetKey("a"))
        {
            rotation.z = 1;
            //rotation.y -= 0.2f;
        }
         if (Input.GetKey ("d"))
        {
            rotation.z = -1;
            //rotation.y += 0.2f;
        }
        if (Input.GetKey ("left shift"))
         upThrust = 1f+thrust;
         if (Input.GetKey ("left ctrl"))
         upThrust = 1f-thrust;
         if (Input.GetKeyDown ("space"))
         tempBall = (GameObject) Instantiate(ball,missileAnchor.position,transform.rotation);
        if (tempBall != null){
            tempBall.GetComponent<Rigidbody>().velocity = rb.velocity + (transform.forward * 5);
        }
        tempBall = null;

    }
    void FixedUpdate(){
      rb.AddRelativeTorque(rotation*rotFactor);
      rb.AddRelativeForce(new Vector3 (rotation.z * sideThrust*(-1f), upThrust*Physics.gravity.magnitude,rotation.x*sideThrust));

      var rotStable = Quaternion.FromToRotation(transform.up, Vector3.up);
      rb.AddTorque(new Vector3(rotStable.x, rotStable.y, rotStable.z)*uprightTorque);
      var rotVel = Quaternion.FromToRotation(transform.forward, rb.velocity);
      rb.AddTorque(new Vector3(0, rotVel.y, 0) * angleTorque*rb.velocity.magnitude);
    }
}
