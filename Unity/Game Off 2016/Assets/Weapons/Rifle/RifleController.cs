﻿using UnityEngine;
using System.Collections;

public class RifleController : WeaponController {

	private Vector3 burstPosition;
	private float burstHeading;

	public int BurstShots = 3;
	public float BurstRateOfFire = 0.075f; //Number of seconds between shots. Lower = Faster

	public float AutoRateOfFire = 0.0875f; //Number of seconds between shots. Lower = Faster
	public float AutoSpread = 0.01f;

	public enum FireMode { BURST_MODE, AUTO_MODE };

	public FireMode Mode = FireMode.BURST_MODE;

	private void FireBurstShot(){
		FireBullet(burstPosition, burstHeading);
	}

	private void FireAutoShot(){
		float Heading = Owner.Heading;
		Heading += Mathf.Lerp(-AutoSpread, AutoSpread, Random.value);
		while(Heading < 0){ Heading += 1; }
		while(Heading > 1){ Heading -= 1; }

		FireBullet(((PlayerController)(Owner)).BulletOriginPoint.transform.position, Heading);
	}

	public override void StartAttack(Vector3 Position, float Heading){
		base.StartAttack(Position, Heading);

		if(Mode == FireMode.BURST_MODE){
			burstHeading = Heading;
			burstPosition = Position;
			for(int i=0; i<BurstShots; i++){
				Invoke("FireBurstShot", i * BurstRateOfFire);
			}

			ActivateCooldown();
			
		} else if(Mode == FireMode.AUTO_MODE){
			InvokeRepeating("FireAutoShot", 0, AutoRateOfFire);
		}
	}

	public override void FinishAttack(Vector3 Position, float Heading){
		base.FinishAttack(Position, Heading);
		if(Mode == FireMode.AUTO_MODE){
			CancelInvoke();
		}
	}
}
