using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
	static GameController instance = null;
	private static int _score = 0;
	public static int Score{
		get { return _score; }
		set {
			_score += value;
		}
	}

	public static void AddPoints(int P){
		_score += P;

		GameObject Score = GameObject.Find("Score");
		Text scoreText = Score.GetComponent<Text>();
		scoreText.text = _score.ToString();
	}
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate game controller self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	public void GoToScene (string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
}
