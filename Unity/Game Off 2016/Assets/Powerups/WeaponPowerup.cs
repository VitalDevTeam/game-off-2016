using UnityEngine;
using System.Collections;

public class WeaponPowerup : MonoBehaviour {
	public GameObject Weapon;

	void OnTriggerEnter2D(Collider2D other){
		PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
		player.Weapon = Weapon;
		Destroy(gameObject);
	}
}
