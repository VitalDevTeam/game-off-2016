using UnityEngine;
using System.Collections;

public class PistolController : WeaponController {
	public override void StartAttack(Vector3 Position, float Heading){
		FireBullet(Position, Heading);
	}
}
