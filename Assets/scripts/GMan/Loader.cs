// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GMan {
/*
	Loader

	A basic loader with a fading veil.
*/
	public class Loader : MonoBehaviour {

		public Image cover;                    		// A veil to mask transitions and block events

		public string currentScene = "chooser";		// The name of the scene loaded at startup

		public float maxFadeTime = 0.5f;       		// Duration of fade-ins and fade-outs

		float time;                            		// Internal clock

		string scene = string.Empty;           		// Current scene

		static Loader Instance;                		// Singleton instance
/*
		instance

		Singleton getter.

		Return
		- Loader: the single instance of the loader.
*/
		public static Loader instance {
			get { return Instance; }
		}
/*
		Start

		The startup scene is loaded if specified.
*/
		void Start() {

			if(Instance == null)
				Instance = this;

			if(currentScene != string.Empty)
				StartCoroutine(LoadScene(currentScene));
		}
/*
		Load

		The next scene is loaded.

		Params
		- sceneName(string): the name of the scene to be loaded.
*/
		public void Load(string sceneName) {
			StartCoroutine(FadeAndLoad(sceneName));
		}
/*
		FadeAndLoad

		Fades the veil out before loading the next scene.

		Params
		- sceneName(string): the name of the scene to be loaded.
*/
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
/*
		LoadScene

		Loads the next scene.

		Params
		- sceneName(string): the name of the scene to be loaded.
*/
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
/*
		FadeIn

		Fades the veil in after loading the next scene.
*/
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
	}
}
