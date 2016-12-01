using UnityEngine;
using System.Collections;

public class EnemyPatroller : MonoBehaviour {
	private Vector3[] _waypoints;
	public Transform[] Waypoints;
	private int activeWaypointIndex = 0;
	SpriteRenderer sprite;

	private EnemyController ec;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		ec = GetComponent<EnemyController>();
		_waypoints = new Vector3[Waypoints.Length];

		for(int i=0; i<Waypoints.Length; i++){
			Vector3 pos = Waypoints[i].position;
			_waypoints[i] = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(ec.Alive) {
			Vector3 waypoint = _waypoints[activeWaypointIndex];
			Vector3 frameDestination = Vector3.MoveTowards(transform.position, waypoint, ec.Speed * Time.deltaTime);

			sprite.flipX = (frameDestination.x > transform.position.x);

			transform.position = frameDestination;
			if(transform.position == waypoint){
				activeWaypointIndex = (activeWaypointIndex + 1) % Waypoints.Length;
			}	
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
