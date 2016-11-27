using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {
	protected const float tau = Mathf.PI * 2;

	protected bool _invincible = false;
	protected bool _alive = true;
	protected float _heading;
	protected SpriteRenderer sprite;
	protected Rigidbody2D rb;
	protected Animator animator;
	protected BoxCollider2D collider;
	protected WeaponController weaponController;
	protected float _health;
	protected GameObject _weapon;

	public bool Alive{ 
		get { return _alive; } 
	}
	public float Heading {
		get { return _heading; }
		set {
			while(value < 0){ value +=1; }
			while(value > 1){ value -=1; }

			_heading = value;
		}
	}

	public float Speed = 3.0f;
	public float MaxHealth = 100.0f;
	public float Health {
		get { return _health; }
		set { _health = Mathf.Clamp(value, 0, MaxHealth); }
	}

	public GameObject Weapon {
		get { return _weapon; }
		set { 
			_weapon = value; 
			weaponController = _weapon.GetComponent<WeaponController>();
			weaponController.Owner = this;
		}
	}

	// Use this for initialization
	public virtual void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer>();

		_alive = true;
        Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void TakeDamage(float Damage){
		if(!_invincible){
			Health -= Damage;
			// Debug.LogFormat("Actor “{0}” took {1} damage! Health is at {2}", gameObject.name, Damage, Health);
			if(Health <= 0){
				Die();
			}
		}
	}

	protected virtual void Die() {
		// Debug.LogFormat("Actor “{0}” died!", gameObject.name);
		// disable BoxCollider2D
		collider.enabled = false;
		// aaannnd... im dead.
		_alive = false;
	}

	public void PointToMouse(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		PointTo(mousePos);
	}

	public void PointTo(Vector3 pos){
		Vector2 V = pos - transform.position;

		if(V.x != 0 || V.y != 0){
			float theta = Mathf.Atan2(V.y, V.x) % tau;
			Heading = theta / tau;
		}
	}
}
