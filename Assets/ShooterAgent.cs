using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ShooterAgent : Agent
{

    Rigidbody rb;
    private gameController GameController;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            GameController = gameControllerObject.GetComponent<gameController>();
        }
        if (gameControllerObject == null) {
            Debug.Log ("Cannot find GameController script");
        }  


    }


    public Transform Target;
    public override void AgentReset()
    {
        if (GameController.gameOver)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3( 0, 0, 0);
        }

        // destroy all asteroids
        /*
        for each asteriod in observation list
            Object.Destroy(gameObject)
        */
        Target.position = new Vector3(Random.value * 8 - 4,
                                      0.5f,
                                      Random.value * 8 - 4);
    }


    public override void CollectObservations()
    {

    /*
    for each asteriod in observation list
        AddVectorObs
    */
    // Target and Agent positions
    AddVectorObs(Target.position);
    AddVectorObs(this.transform.position);

    // Agent velocity
    AddVectorObs(rBody.velocity.x);
    AddVectorObs(rBody.velocity.z);
    }


    public float speed = 10;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 3    left, right, and shoot
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);

        // Rewards handled in gameController.cs

    }
}
