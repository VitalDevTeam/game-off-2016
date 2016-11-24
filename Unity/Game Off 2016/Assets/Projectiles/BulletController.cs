using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float Speed = 6.0f;
	public float Damage = 5.0f;
	public AudioClip CreationSound;
	public AudioClip DeathSound;

	private Renderer r;
	private AudioSource audioSource;
	private Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		r = GetComponent<Renderer>();

		audioSource.clip = CreationSound;
		audioSource.Play();

		float theta = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
		rb.velocity = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * Speed;
	}

	void OnTriggerEnter2D(Collider2D other){
		HitObject(other);
	}

	void HitObject(Collider2D other){
		other.gameObject.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);

		audioSource.clip = DeathSound;
		audioSource.Play();
		r.enabled = false;
		Destroy(this.gameObject, DeathSound.length);
	}
}