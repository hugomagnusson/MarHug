using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 using UnityEngine.SceneManagement;
public class DroneController : MonoBehaviour
{
   public float uprightTorque = 0;
    public float angleTorque = 0;
   private Rigidbody rb;
   private float upThrust;
    private float engineThrust;
    public float sideThrust;
    public Transform missileAnchor;
    public ScoreKeeper sk;
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
    public GameObject ball;
    private GameObject tempBall;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        engineThrust = 1f;
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
         upThrust = 0f;
         rotation = new Vector3(0,0,0);
         if (Input.GetKey ("w"))
         rotation.x = 1;
         if (Input.GetKey ("s"))
         rotation.x = -1;
         if (Input.GetKey ("e"))
         rotation.y = 2f;
         if (Input.GetKey ("q"))
         rotation.y = -2f;
        if (Input.GetKey("a"))
        {
            rotation.z = 1.2f;
            //rotation.y -= 0.2f;
        }
         if (Input.GetKey ("d"))
        {
            rotation.z = -1.2f;
            //rotation.y += 0.2f;
        }
        if (Input.GetKey("left shift"))
        {
            upThrust = 1f;
        }
        if (Input.GetKey("left ctrl"))
        {
            upThrust = -1f;
        }
         if (Input.GetKeyDown ("space"))
         tempBall = (GameObject) Instantiate(ball,missileAnchor.position,missileAnchor.rotation);
        if (tempBall != null){
            tempBall.GetComponent<Rigidbody>().velocity = rb.velocity -(transform.up*1);
        }
        tempBall = null;
        foreach (Animator a in aList)
        {
            a.SetFloat("flapSpeed", engineThrust * (1.2f + (upThrust))*3);
        }
        if (Input.GetKeyDown ("r")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

	
    }
    void FixedUpdate(){
        if(!Physics.Raycast(transform.position, -transform.up, 2)){
            sk.startTime();
        }
        if (upThrust == 1 && engineThrust <= 1)
        {
            engineThrust += (0.0001f+(engineThrust/100))*0.02f/Time.fixedDeltaTime;
        }
        if (engineThrust > 1)
            engineThrust = 1;
        if (upThrust == -1 && Physics.Raycast(transform.position, -transform.up, 2) && engineThrust >= 0)
        {
            engineThrust -= 0.005f*0.02f/Time.fixedDeltaTime;
        }
        if (engineThrust < 0)
            engineThrust = 0;
        
      rb.AddRelativeTorque(rotation*rotFactor);
      rb.AddRelativeForce(new Vector3 (rotation.z * sideThrust*(-1f)*engineThrust, engineThrust*(2+(thrust*upThrust))*Physics.gravity.magnitude/2,rotation.x*sideThrust * engineThrust));

      var rotStable = Quaternion.FromToRotation(transform.up, Vector3.up);
      rb.AddTorque(new Vector3(rotStable.x, rotStable.y, rotStable.z)*uprightTorque);
      float dot = Vector3.Dot(transform.forward,rb.velocity)/(0.0001f + rb.velocity.magnitude);
      if (dot<0){
        dot = 0;
      }
      var rotVel = Quaternion.FromToRotation(transform.forward, rb.velocity);
      rb.AddTorque(new Vector3(0, rotVel.y, 0) * angleTorque*rb.velocity.magnitude*dot);
      
    }
}
