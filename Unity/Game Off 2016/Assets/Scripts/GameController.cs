using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
	private static int _score = 0;
	public static int Score{
		get { return _score; }
		set {
			_score += value;
		}
	}
	public GameObject GameOverScreen;

	public static void AddPoints(int P){
		_score += P;

		GameObject Score = GameObject.Find("Score");
		Text scoreText = Score.GetComponent<Text>();
		scoreText.text = _score.ToString();
	}

	public void GameOver(){
		GameOverScreen.SetActive(true);
	}
	
	void Start () {
		GameOverScreen.SetActive(false);
		_score = 0;
	}

	public void GoToScene (string sceneName) {
		Debug.LogFormat("Go to Scene {0}", sceneName);
		SceneManager.LoadScene(sceneName);
	}
}
