using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	public const string UP = "up";
	public const string RIGHT = "right";
	public const string DOWN = "down";
	public const string LEFT = "left";
	
	private string _direction = DOWN;

	private Rigidbody2D rb;

	public float Speed = 3.0f;

	public string GetDirection(){
		Vector2 V = rb.velocity;
		string ret = "";

		if(V.x == 0 && V.y == 0){
			ret = _direction;
		} else {
			if(Mathf.Abs(V.y) > Mathf.Abs(V.x)){
				if(V.y > 0){
					ret = UP;
				} else {
					ret = DOWN;
				}
			} else {
				if(V.x > 0){
					ret = RIGHT;
				} else {
					ret = LEFT;
				}
			}
		}

		return ret;
	}

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
		_direction = GetDirection();
	}
}
