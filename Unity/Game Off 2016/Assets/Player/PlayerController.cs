using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	private const float tau = Mathf.PI * 2;
	private float _heading = 0.75f;
	private Rigidbody2D rb;
	private Animator animator;
	private bool isAttacking;

	public float Speed = 3.0f;
	public GameObject Attack;

	public float GetHeading(){
		// Sprite faces mouse position
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 V = mousePos - transform.position;
		float ret;

		if(V.x == 0 && V.y == 0){
			ret = _heading;
		} else {
			float theta = (Mathf.Atan2(V.y, V.x) % tau) - (tau/8);
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
		if(!isAttacking) {
			animator.SetBool("Attacking", true);
			isAttacking = true;
		}
		
	}

	void TriggerAttack(){
		Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y) );
		Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 direction = target - playerPosition;
		direction.Normalize();

		Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

		Instantiate(Attack, playerPosition, rotation);
	}

	void FinishAttack(){
		animator.SetBool("Attacking", false);
		isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {
		float dX = Input.GetAxis("Horizontal");
		float dY = Input.GetAxis("Vertical");
		Vector2 V = new Vector2(dX, dY);

		if(Input.GetMouseButton(0)){
			StartAttack();
		}

		float frameSpeed = Speed;
		if (animator.GetBool("Attacking")) {
			frameSpeed *= 0.75f;
		} else if (Input.GetKey(KeyCode.LeftShift)) {
			frameSpeed *= 2.0f;
		}

		rb.velocity = V.normalized * frameSpeed;
		_heading = GetHeading();

		animator.SetFloat("Heading", _heading);
		animator.SetFloat("Speed", rb.velocity.magnitude);
	}

	void Shoot(){
		print("I've been shot!");
	}
}
