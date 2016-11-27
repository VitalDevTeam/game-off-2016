using UnityEngine;
using System.Collections;

public class HealthPowerup : MonoBehaviour {
	public float Health = 20;

	void OnTriggerEnter2D(Collider2D other){
		PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
		player.Health += Health;
		Destroy(gameObject);
	}
}
