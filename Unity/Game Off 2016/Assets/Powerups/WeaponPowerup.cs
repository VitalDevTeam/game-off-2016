using UnityEngine;
using System.Collections;

public class WeaponPowerup : MonoBehaviour {
	public GameObject Weapon;
	public AudioClip PickupSound;
	private AudioSource audioSource;
	private SpriteRenderer sprite;
	private Collider2D collider;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
		sprite = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();
	}

	void OnTriggerEnter2D(Collider2D other){
		PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
		player.Weapon = Weapon;
		collider.enabled = false;
		sprite.enabled = false;

		audioSource.clip = PickupSound;
		audioSource.Play();

		Destroy(gameObject, PickupSound.length);
	}
}
