using UnityEngine;
using System.Collections;

public class EnemyRandomShooter : MonoBehaviour {
	private GameObject Player;
	private WeaponController _weapon;
	public GameObject Weapon;
	private EnemyController ec;
	private bool _attacking = false;

	public float AggressionLevel = 1f;
	public GameObject Target;
	
	void Start(){
		Player = GameObject.Find("Player");
		Target = Player;
		ec = GetComponent<EnemyController>();
		_weapon = Weapon.GetComponent<WeaponController>();
	}

	void FinishAttack(){
		_weapon.FinishAttack(gameObject.transform.position, ec.Heading);
		_attacking = false;
	}

	void StartAttack(){
		_weapon.StartAttack(gameObject.transform.position, ec.Heading);
		_attacking = true;
	}

	void Update () {
		if(ec.Alive){
			ec.PointTo(Target.transform.position);
			if(Random.value <= AggressionLevel && !_attacking){
				StartAttack();
				Invoke("FinishAttack", _weapon.CooldownTime);
			}
		}
	}
}
