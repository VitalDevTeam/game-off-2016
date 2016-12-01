using UnityEngine;
using System.Collections;

public class HealthPowerup : MonoBehaviour {
	public float Health = 20;

	void OnTriggerEnter2D(Collider2D other){
		PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
		player.Health += Health;

		AudioSource audio = GetComponent<AudioSource>();
		AudioClip clip = audio.clip;
		audio.Play();
		GetComponent<Collider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;

		Destroy(gameObject, clip.length);
	}
}
