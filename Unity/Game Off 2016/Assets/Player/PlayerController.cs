using UnityEngine;
using System.Collections;


public class PlayerController : ActorController {
    public int damagePerHit = 20;

	public override void Start () {
		base.Start();
	}

	void StartAttack(){
		weaponController.StartAttack(gameObject.transform.position, _heading);
		animator.SetBool("Attacking", true);
	}

	void FinishAttack(){
		weaponController.FinishAttack(gameObject.transform.position, _heading);
		animator.SetBool("Attacking", false);
	}
	
	void Update () {
		if(Alive) {
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
			PointToMouse();
			
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
		if(Alive) {
			collider.enabled = true;
		}
    }

	void Die() {
		// disable BoxCollider2D
		collider.enabled = false;
		// aaannnd... im dead.
		_alive = false;
	}
	
}
