using UnityEngine;
using System.Collections;

public class EnemyController : ActorController {

    public int damagePerShot = 20;

	void Start () { 
        base.Start();
	}
	
	void Update () {
	
	}

	void Shoot(){

		// If the enemy is dead...
        if(Alive) {
        
            // Play hit animation
            StartCoroutine(Hit());

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= damagePerShot;

            // If the current health is less than or equal to zero...
            if(currentHealth <= 0) {
                // ... the enemy is dead.
                Die ();
            }

            Debug.LogFormat("Oh my God, you shot the {0}!", gameObject.name);

        }
            
	}

    IEnumerator Hit() {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Hit", false);
    }

	void Die () {
        _alive = false;
        collider.enabled = false;
        animator.SetBool("Dead", true);
    }

    void FinishDying () {
        Destroy(this.gameObject);
		Debug.LogFormat("Enemy is Dead", gameObject.name);
    }

}
