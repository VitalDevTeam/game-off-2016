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
	public GameObject GameOverScreen;
	public RectTransform HealthMeter;
	private float HealthMeterWidth;

	public static void AddPoints(int P){
		_score += P;

		GameObject Score = GameObject.Find("Score");
		Text scoreText = Score.GetComponent<Text>();
		scoreText.text = _score.ToString();
	}

	public static void UpdateHealthMeter(float Health){
		instance.HealthMeter.sizeDelta = new Vector2(700.0f * Health, 84.0f);
	}

	public void GameOver(){
		GameOverScreen.SetActive(true);
	}

	void Awake() {
		GameController.instance = this;
	}
	
	void Start () {
		HealthMeterWidth = HealthMeter.rect.width;
		GameOverScreen.SetActive(false);
		_score = 0;
	}

	public void GoToScene (string sceneName) {
		Debug.LogFormat("Go to Scene {0}", sceneName);
		SceneManager.LoadScene(sceneName);
	}
}
