using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {
	protected const float tau = Mathf.PI * 2;

	protected bool _alive = true;
	protected float _heading;
	protected Rigidbody2D rb;
	protected Animator animator;
	protected BoxCollider2D collider;
	protected WeaponController weaponController;

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
	public int startingHealth = 100;
	public int currentHealth;
	public GameObject Weapon;

	// Use this for initialization
	public virtual void Start () {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

		if(Weapon) {
			weaponController = Weapon.GetComponent<WeaponController>();
			weaponController.Owner = this;
		}

		_alive = true;
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
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
