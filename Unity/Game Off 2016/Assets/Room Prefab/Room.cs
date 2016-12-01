using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
	public GameObject CameraBounds;
	private Camera MainCamera;
	private CameraController cameraController;
	public AudioClip BackgroundMusic;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		MainCamera = Camera.main;
		cameraController = MainCamera.GetComponent<CameraController>();
		audioSource = GetComponent<AudioSource>();
	}

	void EnterRoom(){
		cameraController.CurrentRoom = this;
		if(BackgroundMusic){
			audioSource.clip = BackgroundMusic;
			audioSource.Play();
		}
	}

	void ExitRoom(){
		audioSource.Stop();
	}
}
