﻿using UnityEngine;
using System.Collections;


public class PlayerController : ActorController {
	private AudioSource audio;
	public AudioClip[] PainSounds;
	public GameObject DefaultWeapon;
	public GameObject WeaponSpriteContainer;
	public GameObject BulletOriginPoint;
	private SpriteRenderer WeaponSprite;
	private CameraController camera;

	public override GameObject Weapon {
		get { return _weapon; }
		set { 
			if(_weapon){
				WeaponController oldController = _weapon.GetComponent<WeaponController>();
				if(oldController.IsAttacking){
					oldController.FinishAttack(transform.position, Heading);
				}
			}
			_weapon = value; 
			weaponController = _weapon.GetComponent<WeaponController>();
			if(weaponController){
				weaponController.Owner = this;
				WeaponSprite.sprite = weaponController.sprite;
			}
		}
	}
	
	public override void Start () {
		base.Start();
		audio = GetComponent<AudioSource>();
		WeaponSprite = WeaponSpriteContainer.GetComponent<SpriteRenderer>();
		Weapon = DefaultWeapon;
		camera = Camera.main.GetComponent<CameraController>();
	}

	void StartAttack(){
		if(weaponController){
			weaponController.StartAttack(BulletOriginPoint.transform.position, _heading);
			animator.SetBool("Attacking", true);
		}
	}

	void FinishAttack(){
		if(weaponController){
			weaponController.FinishAttack(BulletOriginPoint.transform.position, _heading);
			animator.SetBool("Attacking", false);
		}
	}

	void LateUpdate(){
		float theta = (_heading * 360) - 90;
		while(theta < 0){
			theta += 360;
		}

		transform.rotation = Quaternion.Euler(0, 0, theta);
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(WeaponSpriteContainer.transform.position, 0.15f);
	}
	
	void Update () {
		if(Alive) {
			if(_invincible){
				sprite.enabled = !sprite.enabled;
			} else {
				sprite.enabled = true;
			}

			float dX = Input.GetAxis("Horizontal");
			float dY = Input.GetAxis("Vertical");
			float frameSpeed = Speed;
			Vector2 V = new Vector2(dX, dY);

			if(Input.GetMouseButtonDown(0) && !weaponController.Cooldown){
				StartAttack();
			} else if(Input.GetMouseButtonUp(0)){
				FinishAttack();
			}

			if (weaponController && weaponController.IsAttacking) {
				weaponController.ContinueAttack(gameObject.transform.position, _heading);
				frameSpeed *= 0.75f;
			} else if (Input.GetKey(KeyCode.LeftShift)) {
				frameSpeed *= 2.0f;
			}

			rb.velocity = V.normalized * frameSpeed;
			PointToMouse();
			
			animator.SetFloat("Heading", _heading);
			animator.SetFloat("Speed", rb.velocity.magnitude);
			GameController.UpdateHealthMeter(Health / MaxHealth);
		} else {
			rb.velocity = Vector2.zero;
			if (weaponController && weaponController.IsAttacking) {
				weaponController.FinishAttack(gameObject.transform.position, _heading);
			}
		}
	}

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
		if(Alive){
			StartCoroutine(Hit());
		}
	}

 	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.layer == 10){ //Enemies
			EnemyController ec = col.gameObject.GetComponent<EnemyController>();
			TakeDamage(ec.TouchDamage);
		}
    }

	protected override void Die(){
		weaponController.FinishAttack(transform.position, Heading);

		base.Die();
		GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
		gc.GameOver();
	}

    IEnumerator Hit() {
		// temporarily invincible so player can't get hit again immediately.
		_invincible = true;

		camera.Shake = 0.125f;

		AudioClip cry;
		cry = PainSounds[Random.Range(0, PainSounds.Length)];
		audio.clip = cry;
		audio.Play();

		// blink sprite three times
		GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
		GetComponent<Renderer>().enabled = true;

		// wait a bit
        yield return new WaitForSeconds(1f);
		
		_invincible = false;
    }
	
}
