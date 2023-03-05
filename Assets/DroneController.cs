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

    public Animator a1;
    public Animator a2;
    public Animator a3;
    public Animator a4;
    public Animator a5;
    public Animator a6;
    public Animator a7;
    public Animator a8;
    List<Animator> aList;
    public float thrust = 0.5f; 
   public float rotFactor;
   private Vector3 rotation;
   public Object ball;
    private GameObject tempBall;
    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //thrust = 0;
        aList = new List<Animator>();
        aList.Add(a1);
        aList.Add(a2);
        aList.Add(a3);
        aList.Add(a4);
        aList.Add(a5);
        aList.Add(a6);
        aList.Add(a7);
        aList.Add(a8);

    }

    // Update is called once per frame
    void Update()
    {
        speed = 1.5f;
         upThrust = 0f;
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
        if (Input.GetKey("left shift"))
        {
            upThrust = 0f + thrust;
            speed = 4;
        }
        if (Input.GetKey("left ctrl"))
        {
            speed = 1;
            upThrust = 1f - thrust;
        }
         if (Input.GetKeyDown ("space"))
         tempBall = (GameObject) Instantiate(ball,missileAnchor.position,transform.rotation);
        if (tempBall != null){
            tempBall.GetComponent<Rigidbody>().velocity = rb.velocity + (transform.forward * 5);
        }
        tempBall = null;
        foreach (Animator a in aList)
        {
            a.SetFloat("flapSpeed", speed);
        }
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
