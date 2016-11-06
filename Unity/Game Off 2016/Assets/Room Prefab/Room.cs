using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
	public GameObject CameraBounds;
	private Camera MainCamera;
	private CameraController cameraController;

	// Use this for initialization
	void Start () {
		MainCamera = Camera.main;
		cameraController = MainCamera.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void EnterRoom(){
		cameraController.CurrentRoom = this;
	}
}
