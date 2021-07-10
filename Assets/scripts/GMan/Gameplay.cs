// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Gameplay

	Main component of the gameplay scene.
*/
	public class Gameplay : MonoBehaviour {

		public Material skybox;      	// Skybox used by this scene

		public Animator avatarSample;	// Animated preview
/*
		Start

		UI is updated with the loop data on PlayerPrefs.
*/
		void Start() {

			RenderSettings.skybox = skybox;

			if(PlayerPrefs.HasKey("loop_name"))
				avatarSample.Play(PlayerPrefs.GetString("loop_name"));
		}
/*
		RespawnItem

		Makes an item reappear.

		Params
		- item(Item): the item to be respawned.
*/
		public void RespawnItem(Item item) {
			item.gameObject.SetActive(false);
			StartCoroutine(Respawn(item));
		}
/*
		Respawn

		Makes an item reappear after 3 seconds.

		Params
		- item(Item): the item to be respawned.
*/
		IEnumerator Respawn(Item item) {

			yield return new WaitForSeconds(3);

			item.gameObject.SetActive(true);
		}
	}
}
