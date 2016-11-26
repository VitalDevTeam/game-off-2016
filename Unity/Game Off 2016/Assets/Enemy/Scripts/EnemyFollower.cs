using UnityEngine;
using System.Collections;

public class EnemyFollower : MonoBehaviour {
	private Transform player; 
	private EnemyController ec;
	public float speed = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
		ec = GetComponent<EnemyController>();
	}

	void Update () {
		ec.PointTo(player.position);
        transform.rotation = Quaternion.Euler(0, 0, ec.Heading * 360);
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}
	
}
