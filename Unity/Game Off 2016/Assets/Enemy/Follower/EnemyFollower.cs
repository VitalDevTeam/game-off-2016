using UnityEngine;
using System.Collections;

public class EnemyFollower : MonoBehaviour {
	private Transform player; 
	private EnemyController ec;
	public float Speed = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
		ec = GetComponent<EnemyController>();
	}

	void Update () {
		ec.PointTo(player.position);
		float theta = (ec.Heading * 360) + 90;
		while(theta < 0){
			theta += 360;
		}

		transform.rotation = Quaternion.Euler(0, 0, theta);

		float frameSpeed = Speed * Time.deltaTime;
		if(!ec.Alive){
			frameSpeed *= 0.25f;
		}
		transform.position = Vector2.MoveTowards(transform.position, player.position, frameSpeed);
	}
	
}
