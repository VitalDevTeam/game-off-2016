using UnityEngine;
using System.Collections;

public class EnemyPatroller : MonoBehaviour {
	public Transform[] Waypoints;
	private int activeWaypointIndex = 0;

	private EnemyController ec;

	// Use this for initialization
	void Start () {
		ec = GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {

		if(ec.Alive) {

			Transform waypoint = Waypoints[activeWaypointIndex];
			transform.position = Vector3.MoveTowards(transform.position, waypoint.position, ec.Speed * Time.deltaTime);
			if(transform.position == waypoint.position){
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
