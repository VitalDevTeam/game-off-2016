using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour {
	public void Complete(){
		SceneManager.LoadScene("Title Screen");
	}

	public void SayVital(){
		GetComponent<AudioSource>().Play();
	}
}
