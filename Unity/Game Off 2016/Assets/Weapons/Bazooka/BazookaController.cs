using UnityEngine;
using System.Collections;

public class BazookaController : WeaponController {

	private float _charge = 0f;
	private float _chargeStart = 0f;

	public float FullChargeTime = 3.0f;
	
	public override void StartAttack(Vector3 Position, float Heading){
		base.StartAttack(Position, Heading);
		_charge = 0f;
		_chargeStart = Time.time;
	}

	public override void ContinueAttack(Vector3 Position, float Heading){
		_charge = Mathf.Clamp((Time.time - _chargeStart) / FullChargeTime, 0, 1);
	}

	public override void FinishAttack(Vector3 Position, float Heading){
		base.FinishAttack(Position, Heading);
		BulletController bullet = ((GameObject)FireBullet(Position, Heading)).GetComponent<BulletController>();

		bullet.Speed *= Mathf.Max(0.5f, _charge);
		bullet.Damage *= _charge;
		
		ActivateCooldown();
	}
}
