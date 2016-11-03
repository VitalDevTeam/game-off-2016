using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb;

	public float Speed = 3.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float dX = Input.GetAxis("Horizontal");
		float dY = Input.GetAxis("Vertical");
		Vector2 V = new Vector2(dX, dY);

		rb.velocity = V.normalized * Speed;
	}
}
