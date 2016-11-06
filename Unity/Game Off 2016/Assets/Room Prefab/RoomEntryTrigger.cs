using UnityEngine;
using System.Collections;

public class RoomEntryTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			gameObject.transform.parent.SendMessage("EnterRoom");
		}
	}
}
