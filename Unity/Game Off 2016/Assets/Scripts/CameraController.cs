using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float Speed = 0.1f;
	private GameObject Player;
	private Room _currentRoom;
	private Bounds _roomBounds;
	private float verticalExtent;
	private float horizontalExtent;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	public float Shake = 0f;
	public float ShakeAmplitude = 0.125f;
	float ShakeDecay = 1.0f;

	public Room CurrentRoom
	{
		get { return _currentRoom; }
		set {
			_currentRoom = value;
			Bounds bounds = _currentRoom.CameraBounds.GetComponent<BoxCollider2D>().bounds;
			float horizontalScrollExtent = Mathf.Max(0f, (bounds.extents.x - horizontalExtent));
			float verticalScrollExtent = Mathf.Max(0f, (bounds.extents.y - verticalExtent));
			minX = bounds.center.x - horizontalScrollExtent;
			maxX = bounds.center.x + horizontalScrollExtent;
			minY = bounds.center.y - verticalScrollExtent;
			maxY = bounds.center.y + verticalScrollExtent;
		}
	}

	void FollowPlayer(){
		Vector3 target = new Vector3(
			Player.transform.position.x,
			Player.transform.position.y,
			transform.position.z
		);

		transform.position = Vector3.MoveTowards(transform.position, target, Speed);
	}

	void StayInsideRoom(){
		Vector3 v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;
	}

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		verticalExtent = Camera.main.orthographicSize;
		horizontalExtent = verticalExtent * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		FollowPlayer();
	}

	void LateUpdate(){
		if (Shake > 0) {
			transform.localPosition += (Vector3)(Random.insideUnitCircle * ShakeAmplitude);
			Shake -= Time.deltaTime * ShakeDecay;
		
		} else {
			Shake = 0.0f;
		}
		StayInsideRoom();
	} 
}
