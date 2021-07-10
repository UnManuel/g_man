// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Gameplay : MonoBehaviour {

		public Material skybox;

		public Animator avatarSample;

		void Start() {

			RenderSettings.skybox = skybox;

			if(PlayerPrefs.HasKey("loop_name"))
				avatarSample.Play(PlayerPrefs.GetString("loop_name"));
		}

		public void RespawnItem(Item item) {
			StartCoroutine(Respawn(item));
		}

		IEnumerator Respawn(Item item) {

			yield return new WaitForSeconds(3);

			item.gameObject.SetActive(true);
		}
	}
}
