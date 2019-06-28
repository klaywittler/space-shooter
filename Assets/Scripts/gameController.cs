using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {
	public GameObject hazard;
	public Vector3 spawnValue;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	IEnumerator spawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true){
			for (int i=0; i<hazardCount; i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValue.x,spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (spawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
