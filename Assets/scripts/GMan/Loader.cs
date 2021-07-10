// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

	public Image cover;
    
	public string currentScene = "chooser";

	public float maxFadeTime = 0.5f;

	float time;

	static Loader Instance;

	public static Loader instance {
		get { return Instance; }
	}

	string scene = string.Empty;

	void Start() {

		if(Instance == null)
			Instance = this;

		if(currentScene != string.Empty)
			StartCoroutine(LoadScene(currentScene));
	}

	public void Load(string sceneName) {
		StartCoroutine(FadeAndLoad(sceneName));
	}

	IEnumerator FadeAndLoad(string sceneName) {

		time = 0;

		cover.gameObject.SetActive(true);

		while(time < maxFadeTime) {

			time += Time.deltaTime;

			if(time > maxFadeTime)
				time = maxFadeTime;

			Color c = cover.color;
			c.a = time / maxFadeTime;
			cover.color = c;

			yield return null;
		}

		StartCoroutine(LoadScene(sceneName));
	}

	IEnumerator FadeIn() {

		time = 0;

		cover.gameObject.SetActive(true);

		while(time < maxFadeTime) {

			time += Time.deltaTime;

			if(time > maxFadeTime)
				time = maxFadeTime;

			Color c = cover.color;
			c.a = 1f - time / maxFadeTime;
			cover.color = c;

			if(time == maxFadeTime)
				cover.gameObject.SetActive(false);

			yield return null;
		}
	}

	IEnumerator LoadScene(string sceneName) {

		if(scene != string.Empty)
			SceneManager.UnloadSceneAsync(scene);

		currentScene = sceneName;

		if(sceneName != string.Empty) {

			AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			while(!op.isDone)
				yield return null;

			StartCoroutine(FadeIn());
		}

		scene = sceneName;
	}
}
