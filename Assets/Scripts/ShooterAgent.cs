using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ShooterAgent : Agent
{
    private GameController gameController;
    public Transform Target;
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
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
            AddVectorObs(go.localPosition);
        }
        AddVectorObs(this.transform.localPosition);
        
        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public float speed = 10;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);
        
        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition,
                                                  Target.localPosition);
        
        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            Done();
        }
        

       if (gameController.gameOver)
        {
            Done();
        } 
        
    }
}
