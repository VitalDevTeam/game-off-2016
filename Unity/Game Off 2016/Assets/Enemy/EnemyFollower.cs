using UnityEngine;
using System.Collections;

public class EnemyFollower : MonoBehaviour {
	private Transform player; 
	public float speed = 2f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}

	void Update () {
		Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	}
	
}
