using UnityEngine;
using System.Collections;

public interface IWeapon{
	//StartAttack() is called on the frame when 
	//the player first presses the attack button. 
	void StartAttack(Vector3 Position, float Heading);

	//ContinueAttack() is called on each frame
	//after StartAttack() fires, and stops at FinishAttack() 
	void ContinueAttack(Vector3 Position, float Heading);

	//Called on the frame when the player releases the attack button
	void FinishAttack();
}

public class WeaponController : MonoBehaviour, IWeapon {
	private bool _isAttacking;
	public bool IsAttacking {
		get { return _isAttacking; }
	}

	public GameObject Bullet;

	public virtual void StartAttack(Vector3 Position, float Heading){
		_isAttacking = true;
	}

	public virtual void ContinueAttack(Vector3 Position, float Heading){
	}

	public virtual void FinishAttack(){
		_isAttacking = false;
	}
}
