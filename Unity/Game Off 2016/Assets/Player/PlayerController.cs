using UnityEngine;
using System.Collections;


public class PlayerController : ActorController {
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

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
		if(Alive){
			StartCoroutine(Hit());
		}
	}

 	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.layer == 10){ //Enemies
			EnemyController ec = col.gameObject.GetComponent<EnemyController>();
			TakeDamage(ec.TouchDamage);
		}
    }

    IEnumerator Hit() {
		// temporarily invincible so player can't get hit again immediately.
		_invincible = true;

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
			_invincible = false;
		}
    }
	
}
