using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	static GameController instance = null;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate game controller self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void GoToScene (string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
}
