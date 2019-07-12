using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;

    private bool gameOver;
    private bool restart;

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (true){
            for (int i=0; i<hazardCount; i++){
                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValue.x,spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver) {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
    // Use this for initialization
    void Start () {
        restartText = GameObject.FindWithTag("restartTextinCanvas").GetComponent<Text>();
        gameOverText = GameObject.FindWithTag("gameOverTextinCanvas").GetComponent<Text>();
        scoreText = GameObject.FindWithTag("scoreTextinCanvas").GetComponent<Text>();

        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves ());
    }

    void Update () {
        if (restart) {
            if (Input.GetKeyDown (KeyCode.R)) {
                // Application.LoadLevel (Application.loadedLevel);
                SceneManager.LoadScene("main");
            }
        }
    }
    
    // Update is called once per frame
    void UpdateScore () {
        scoreText.text = "Score: " + score;
    }

    public void AddScore (int newScoreValue) {
        score += newScoreValue;
        UpdateScore ();
    }

    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

}
