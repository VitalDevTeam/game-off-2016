using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] Enemies;
	private GameObject Player;
	private float width;
	private float height;
	
	private bool Active = false;
	public float DefaultSpawnPeriod = 4.0f;
	public float SpawnPeriodDecayRate = 0.1f;
	public float MinSpawnPeriod = 1.0f;

	private float SpawnPeriod;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		BoxCollider2D coll = GetComponent<BoxCollider2D>();
		Vector2 collSize = coll.size;
		width = collSize.x * 0.5f;
		height = collSize.y * 0.5f;
		SpawnPeriod = DefaultSpawnPeriod;
	}

	void SpawnEnemy(){
		int i;
		GameObject EnemyPrefab;
		GameObject spawn;

		Vector3 Position = new Vector3(
			Mathf.Lerp(-width, width, Random.value),
			Mathf.Lerp(-height, height, Random.value),
			0
		);

		Position += transform.position;

		i = Random.Range(0, Enemies.Length);
		EnemyPrefab = Enemies[i];
		Debug.LogFormat("Spawning a {0}…", EnemyPrefab.name);
		spawn = (GameObject)Instantiate(EnemyPrefab, Position, Quaternion.identity);
		spawn.transform.parent = transform;

		SpawnPeriod *= (1-SpawnPeriodDecayRate);
		Invoke("SpawnEnemy", SpawnPeriod);
	}

	void Activate(){
		if(!Active){
			Active = true;
			SpawnPeriod = DefaultSpawnPeriod;
		}
		SpawnEnemy();
	}

	void Deactivate(){
		if(Active){
			Active = false;
		}
		CancelInvoke("SpawnEnemy");
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == Player){
			print("Player touched the spawn area! Start spawning enemies!");
			Activate();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject == Player){
			print("Player left the spawn area! Stop spawning enemies!");
			Deactivate();
		}
	}
}
