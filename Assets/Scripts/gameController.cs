using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount = 10;
    public float spawnWait = 0.5f;
    public float startWait = 1.0f;
    public float waveWait = 4.0f;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;

    private bool gameOver;
    public bool restart;

    public List<GameObject> observations = new List<GameObject>();

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (true){
            for (int i=0; i<hazardCount; i++){
                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValue.x,spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                observations.Add(hazard);
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
