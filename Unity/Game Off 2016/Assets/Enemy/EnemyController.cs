using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Shoot(){
		Debug.LogFormat("Oh my God, you shot {0}! You Bastard!", gameObject.name);
	}
}
