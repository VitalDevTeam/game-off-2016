using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	private const float tau = Mathf.PI * 2;
	private float _heading = 0.75f;
	private Rigidbody2D rb;
	private Animator animator;
	private bool isAttacking;
	private BoxCollider2D collider;

	public float Speed = 3.0f;
	public GameObject Attack;
	public int startingHealth = 100;
	public int currentHealth;
    public int damagePerHit = 20;
	public bool alive;

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
        collider = GetComponent<BoxCollider2D>();
        currentHealth = startingHealth;
		alive = true;
	}

	void StartAttack(){
		if(alive == true) {
			animator.SetBool("Attacking", true);
			isAttacking = true;
		}
	}

	void TriggerAttack(AnimationEvent e){
		if(e.animatorClipInfo.weight > 0.5f){
			Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y) );
			Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
			Vector2 direction = target - playerPosition;
			Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

			Instantiate(Attack, playerPosition, rotation);
		}
	}

	void FinishAttack(){
		animator.SetBool("Attacking", false);
		isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {
			float dX = Input.GetAxis("Horizontal");
			float dY = Input.GetAxis("Vertical");
			float frameSpeed = Speed;
			Vector2 V = new Vector2(dX, dY);

			if(Input.GetMouseButton(0)){
				StartAttack();
			}

			if (animator.GetBool("Attacking")) {
				frameSpeed *= 0.75f;
			} else if (Input.GetKey(KeyCode.LeftShift)) {
				frameSpeed *= 2.0f;
			}

			if(alive == true) {
				rb.velocity = V.normalized * frameSpeed;
			} else {
				rb.velocity = Vector2.zero;
			}

			_heading = GetHeading();
			
			animator.SetFloat("Heading", _heading);
			animator.SetFloat("Speed", rb.velocity.magnitude);
	}

	void Shoot(){
		print("I've been shot!");
	}

 	void OnCollisionEnter2D(Collision2D col) {
		// Take damage if you touch an enemy
		if (col.gameObject.name == "Patroller") {

				StartCoroutine(Hit());
				currentHealth -= damagePerHit;

				if(currentHealth <= 0) {
					Die();
				}
			Debug.LogFormat("I've been hit! Life is at {0}", currentHealth);

		}
    }
    IEnumerator Hit() {
		// disable collider so player can't get hit again immediately.
		collider.enabled = false;

		// blink sprite three times
		GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = true;

		// wait a bit
        yield return new WaitForSeconds(1f);
		
		// if still alive, enable collider so plater can take more damage
		if(alive == true) {
			collider.enabled = true;
		}
    }

	void Die() {
		// disable BoxCollider2D
		collider.enabled = false;
		// aaannnd... im dead.
		alive = false;
	}
	
}
