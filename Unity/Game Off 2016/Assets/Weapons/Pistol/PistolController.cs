using UnityEngine;
using System.Collections;

public class PistolController : WeaponController {
	public override void StartAttack(Vector3 Position, float Heading){
		base.StartAttack(Position, Heading);
		Quaternion Rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, Heading));
		Instantiate(Bullet, Position, Rotation);
	}
}
