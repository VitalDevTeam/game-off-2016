using UnityEngine;
using System.Collections;

public class ShotgunController : WeaponController {

	public int PelletCount = 8;
	public float Spread = 0.041667f; 

	public override void StartAttack(Vector3 Position, float Heading){
		int i;
		float halfArc = Spread * 0.5f;
		float pelletHeading;

		for(i=0; i<PelletCount; i++){
			pelletHeading = Heading + Mathf.Lerp(-halfArc, halfArc, (float)i / (PelletCount-1));
			while(pelletHeading > 1){ pelletHeading -= 1; }
			while(pelletHeading < 0){ pelletHeading += 1; }

			FireBullet(Position, pelletHeading);
		}
	}
}
