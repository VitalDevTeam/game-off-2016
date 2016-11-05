using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	public const int UP = 1;
	public const int RIGHT = 0;
	public const int DOWN = 3;
	public const int LEFT = 2;
	
	private int _direction = 3;

	private Rigidbody2D rb;
	private Animator animator;

	public float Speed = 3.0f;

	public int GetDirection(){
		Vector2 V = rb.velocity;
		int ret;

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
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float dX = Input.GetAxis("Horizontal");
		float dY = Input.GetAxis("Vertical");
		Vector2 V = new Vector2(dX, dY);

		rb.velocity = V.normalized * Speed;
		_direction = GetDirection();

		animator.SetFloat("Direction", (float)_direction);
		animator.SetFloat("Speed", rb.velocity.magnitude);
	}
}
