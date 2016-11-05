using UnityEngine;
using System.Collections;

public class RoomMarker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			print("Go to room " + this.gameObject.name);
			Camera.main.transform.position = new Vector3(
				this.transform.position.x,
				this.transform.position.y,
				Camera.main.transform.position.z
			);
		}
	}
}
