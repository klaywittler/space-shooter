using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GameObject observations;

    public GUIText scoreText;
    private int score;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;


    IEnumerator spawnWaves() 
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i=0; i<hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValue.x,spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


        }
    }

    // Use this for initialization
    void Start () 
    {
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine (spawnWaves ());
    }

    void Update () 
    {

    }
    
    // Update is called once per frame
    void UpdateScore () 
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore (int newScoreValue) 
    {
        score += newScoreValue;
        shooter.SetReward(newScoreValue);
        UpdateScore ();
    }

    public void GameOver() 
    {
        gameOverText.text = "Game Over!"
        gameOver = true;
        shooter.Done();
    }

}
