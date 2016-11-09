using UnityEngine;
using System.Collections;

public class EnemyPatroller : MonoBehaviour {
	public float Speed = 2.0f;
	public Transform[] Waypoints;
	private int activeWaypointIndex = 0;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Transform waypoint = Waypoints[activeWaypointIndex];
		transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Speed * Time.deltaTime);
		if(transform.position == waypoint.position){
			activeWaypointIndex = (activeWaypointIndex + 1) % Waypoints.Length;
		}
	}

	void OnDrawGizmos(){
		int i;
		int l = Waypoints.Length;

		for(i=0; i<l; i++){
			Transform waypoint = Waypoints[i];
			Gizmos.DrawWireSphere(waypoint.position, 0.25f);
		}
	}

}
