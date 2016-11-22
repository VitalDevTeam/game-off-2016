using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	private const float tau = Mathf.PI * 2;
	private float _heading = 0.75f;
	private Rigidbody2D rb;
	private Animator animator;
	private bool isAttacking;
	private BoxCollider2D collider;

	private WeaponController weaponController;


	public GameObject Weapon;

	public float Speed = 3.0f;
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
        collider = GetComponent<BoxCollider2D>();

		weaponController = Weapon.GetComponent<WeaponController>();
		weaponController.Owner = this;

        currentHealth = startingHealth;
		alive = true;
	}

	void StartAttack(){
		weaponController.StartAttack(gameObject.transform.position, _heading);
		animator.SetBool("Attacking", true);
	}

	void FinishAttack(){
		weaponController.FinishAttack();
		animator.SetBool("Attacking", false);
	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
			float dX = Input.GetAxis("Horizontal");
			float dY = Input.GetAxis("Vertical");
			float frameSpeed = Speed;
			Vector2 V = new Vector2(dX, dY);

			if(Input.GetMouseButtonDown(0) && !weaponController.Cooldown){
				StartAttack();
			} else if(Input.GetMouseButtonUp(0)){
				FinishAttack();
			}

			if (weaponController.IsAttacking) {
				weaponController.ContinueAttack(gameObject.transform.position, _heading);
				frameSpeed *= 0.75f;
			} else if (Input.GetKey(KeyCode.LeftShift)) {
				frameSpeed *= 2.0f;
			}

			rb.velocity = V.normalized * frameSpeed;
			_heading = GetHeading();
			
			animator.SetFloat("Heading", _heading);
			animator.SetFloat("Speed", rb.velocity.magnitude);
		} else {
			rb.velocity = Vector2.zero;
		}
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
