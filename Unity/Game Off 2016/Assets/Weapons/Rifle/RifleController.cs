using UnityEngine;
using System.Collections;

public class RifleController : WeaponController {

	private Vector3 burstPosition;
	private float burstHeading;

	public int BurstShots = 3;
	public float BurstRateOfFire = 0.075f; //Number of seconds between shots. Lower = Faster
	public float AutoRateOfFire = 0.0875f; //Number of seconds between shots. Lower = Faster

	public enum FireMode { BURST_MODE, AUTO_MODE };

	public FireMode Mode = FireMode.BURST_MODE;

	private void FireBurstShot(){
		FireBullet(burstPosition, burstHeading);
	}

	private void FireAutoShot(){
		FireBullet(Owner.transform.position, Owner.GetHeading());
	}

	public override void StartAttack(Vector3 Position, float Heading){
		if(Mode == FireMode.BURST_MODE){
			burstHeading = Heading;
			burstPosition = Position;
			for(int i=0; i<BurstShots; i++){
				Invoke("FireBurstShot", i * BurstRateOfFire);
			}
		} else if(Mode == FireMode.AUTO_MODE){
			InvokeRepeating("FireAutoShot", 0, AutoRateOfFire);
		}
	}

	public override void ContinueAttack(Vector3 Position, float Heading){

	}

	public override void FinishAttack(){
		CancelInvoke();
	}
}
