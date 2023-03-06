using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float moveRadius = 20f;
    private float killRadius = 3f;
    public float thrust;
    public float timeToDestroy;
    public GameObject explosion;
    private ScoreKeeper sk;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        timeToDestroy = 20f;
        sk = GameObject.Find("GameManager").GetComponent<ScoreKeeper>();
    }
    void OnCollisionEnter(Collision collision){
        
        
        Collider[] destroyColliders = Physics.OverlapSphere(transform.position,killRadius);
        foreach (Collider obj in destroyColliders){
            Rigidbody objRb = obj.GetComponent<Rigidbody>();
            if (objRb != null && objRb.gameObject.tag == "Shootable"){
                Destroy(objRb.gameObject);
                sk.IncreaseScore(1);
            }
        }
        //sk.IncreaseScore(-1);

        Collider[] moveColliders = Physics.OverlapSphere(transform.position,moveRadius);
        foreach (Collider obj in moveColliders){
            Rigidbody objRb = obj.GetComponent<Rigidbody>();
            if (objRb != null){
                objRb.AddExplosionForce(700f,transform.position,moveRadius);
            }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToDestroy -= Time.fixedDeltaTime;
        

        rb.AddForce(transform.forward*thrust);

        if (timeToDestroy<=0)
        Destroy(gameObject);
    }
}
