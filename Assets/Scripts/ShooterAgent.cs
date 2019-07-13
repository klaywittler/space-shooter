using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ShooterAgent : Agent
{
    private GameController gameController;
    public float speed = 10.0f;
    public Boundary boundary;
    public float tilt = 3.0f;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;
    private float nextFire;

    private AudioSource audioSource;

    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rBody = GetComponent<Rigidbody>();
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {

            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameControllerObject == null) {
            Debug.Log ("Cannot find gameController script");
        }
    }

    public override void AgentReset()
    {
        // If the Agent fell, zero its momentum
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3( 0, 0, 0);
    
        // destroy all asteroids
        foreach(GameObject go in gameController.observations){
            Object.Destroy(go);
        }

    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        foreach(GameObject go in gameController.observations){
            AddVectorObs(go.transform.localPosition);
            AddVectorObs(go.GetComponent<Rigidbody>().velocity.x);
            AddVectorObs(go.GetComponent<Rigidbody>().velocity.z);
        }
        AddVectorObs(this.transform.localPosition);
        
        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 3
        Debug.Log ("vectorAction");
        Debug.Log (vectorAction);
        float moveHorizontal = vectorAction[0];
        float moveVertical = vectorAction[1];
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rBody.velocity = movement * speed;
        
        rBody.position = new Vector3 (
            Mathf.Clamp (rBody.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp (rBody.position.z, boundary.zMin, boundary.zMax)
        );
        
        rBody.rotation = Quaternion.Euler (0, 0, rBody.velocity.x * -tilt);

        if (Time.time > nextFire) {
            if (vectorAction[2] == 1.0f) {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioSource.Play();
            }
        }

       if (gameController.restart)
        {
            Done();
        } 
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boundary" || other.tag == "Player")
        {
            return;
        }
        SetReward(10.0f);

    }
}

