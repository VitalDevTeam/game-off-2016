using UnityEngine;
using System.Collections;

public class EnemyRandomShooter : MonoBehaviour {
	private GameObject Player;
	private WeaponController Weapon;
	private EnemyController ec;
	private bool _attacking = false;

	public float AggressionLevel = 1f;
	public GameObject Target;
	
	void Start(){
		ec = GetComponent<EnemyController>();
		Weapon = ec.Weapon.GetComponent<WeaponController>();
		Player = GameObject.Find("Player");
		Target = Player;
	}

	void FinishAttack(){
		Weapon.FinishAttack(gameObject.transform.position, ec.Heading);
		_attacking = false;
	}

	void StartAttack(){
		Weapon.StartAttack(gameObject.transform.position, ec.Heading);
		_attacking = true;
	}

	void Update () {
		if(ec.Alive){
			ec.PointTo(Target.transform.position);
			if(Random.value <= AggressionLevel && !_attacking){
				StartAttack();
				Invoke("FinishAttack", Weapon.CooldownTime);
			}
		}
	}
}
