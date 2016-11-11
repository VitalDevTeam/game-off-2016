using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float Speed = 6.0f;
	
	private Rigidbody2D rb;

	void Start(){
		float theta = transform.rotation.eulerAngles.z * Mathf.PI / 180;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * Speed;
	}
	
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.LogFormat("Bullet triggered with “{0}”", other.gameObject.name);
		HitObject(other);
	}

	void HitObject(Collider2D other){
		other.gameObject.SendMessage("Shoot", null, SendMessageOptions.DontRequireReceiver);
		Destroy(this.gameObject);
	}
}