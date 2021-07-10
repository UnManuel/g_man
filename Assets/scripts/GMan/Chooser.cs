// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GMan {

	public class Chooser : MonoBehaviour {

		public Animator avatarSample;

		public string[] loopNames;
		public Button[] loopButtons;

		public Color buttonColor, hiliteColor;
		
		int buttonIndex = 0;

		void Start() {
			
			if(PlayerPrefs.HasKey("loop_index")) {
				buttonIndex = PlayerPrefs.GetInt("loop_index");
				avatarSample.Play(PlayerPrefs.GetString("loop_name"));
			}

			loopButtons[buttonIndex].image.color = hiliteColor;
		}

		public void ChangeLoop(int loopIndex) {

			loopButtons[buttonIndex].image.color = buttonColor;
			buttonIndex = loopIndex;
			loopButtons[buttonIndex].image.color = hiliteColor;

			avatarSample.CrossFadeInFixedTime(loopNames[loopIndex], 0.5f);
	    }

	    public void Goto(string sceneName) {

			PlayerPrefs.SetInt("loop_index", buttonIndex);
			PlayerPrefs.SetString("loop_name", loopNames[buttonIndex]);
			
			if(Loader.instance != null)
				Loader.instance.Load(sceneName);
	    }
	}
}
