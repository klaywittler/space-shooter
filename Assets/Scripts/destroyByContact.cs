using UnityEngine;
using System.Collections;

public class destroyByContact : MonoBehaviour {
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private gameController GameController;
    // private ShooterAgent shooter;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null) {
            GameController = gameControllerObject.GetComponent<gameController>();
        }
        if (gameControllerObject == null) {
            Debug.Log ("Cannot find GameController script");
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player") {
            Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
            GameController.GameOver();
            // shooter.AgentReset();
        }
        GameController.AddScore (scoreValue);
        Object.Destroy(other.gameObject);
        Object.Destroy(gameObject);
}
}
