using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public float speed = 10.0f;
    public Boundary boundary;
    public float tilt = 3.0f;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f;
    private float nextFire;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Jump") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            audioSource.Play();
            // create code here that animates the newProjectile

        }
    }


    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        
        rb.position = new Vector3 (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
        
        rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
    }
}
