using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int damagePerShot = 20;              // The damage inflicted by each bullet.
    public bool isDead;                         // Whether the enemy is dead.

    private Animator animator;
    private BoxCollider2D collider;

	// Use this for initialization
	void Start () { 
		
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Shoot(){

		// If the enemy is dead...
        if(isDead)
            // no need to take damage so exit the function.
            return;
        
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

    IEnumerator Hit() {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Hit", false);
    }

	void Die () {
        isDead = true;
        collider.enabled = false;
        animator.SetBool("Dead", true);
    }

    void FinishDying () {
        Destroy(this.gameObject);
		Debug.LogFormat("Enemy is Dead", gameObject.name);
    }

}
