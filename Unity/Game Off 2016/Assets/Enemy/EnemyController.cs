using UnityEngine;
using System.Collections;

public class EnemyController : ActorController {
    public float TouchDamage = 5.0f;
    
	public override void Start () { 
        base.Start();
	}

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
		if(Alive){
			StartCoroutine(Hit());
		} else {
            Die();
        }
	}

    IEnumerator Hit() {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Hit", false);
    }

	void Die () {
		Debug.LogFormat("Enemy “{0}” died!", gameObject.name);
        animator.SetBool("Dead", true);
    }

    void FinishDying () {
        Destroy(this.gameObject);
		Debug.LogFormat("Enemy is Dead", gameObject.name);
    }

}
