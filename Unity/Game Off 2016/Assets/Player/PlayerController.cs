using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	private const float tau = Mathf.PI * 2;
	private float _heading = 0.75f;
	private Rigidbody2D rb;
	private Animator animator;

	public float Speed = 3.0f;

	public float GetHeading(){
		Vector2 V = rb.velocity;
		float ret;

		if(V.x == 0 && V.y == 0){
			ret = _heading;
		} else {
			float theta = Mathf.Atan2(V.y, V.x) % tau;
			while(theta < 0){
				theta += tau;
			}
			
			ret = theta / tau;
		}

		return ret;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void StartAttack(){
		animator.SetBool("Attacking", true);
	}

	void FinishAttack(){
		animator.SetBool("Attacking", false);
	}
	
	// Update is called once per frame
	void Update () {
		float dX = Input.GetAxis("Horizontal");
		float dY = Input.GetAxis("Vertical");
		Vector2 V = new Vector2(dX, dY);

		if(Input.GetKeyDown(KeyCode.Space)){
			StartAttack();
		}

		float frameSpeed = Speed;
		if(animator.GetBool("Attacking")){
			frameSpeed *= 0.25f;
		}

		rb.velocity = V.normalized * frameSpeed;
		_heading = GetHeading();

		animator.SetFloat("Heading", _heading);
		animator.SetFloat("Speed", rb.velocity.magnitude);
	}
}
