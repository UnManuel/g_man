// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GMan {
/*
	Chooser

	A component to choose between several animation loops.
	The chosen animation is stored on PlayerPrefs.
*/
	public class Chooser : MonoBehaviour {

		public Animator avatarSample;         	// The animator with all of the loops

		public string[] loopNames;            	// Names of the loops being used
		public Button[] loopButtons;          	// References to the loop buttons

		public Color buttonColor, hiliteColor;	// Normal and selected button colors
		
		int buttonIndex = 0;                  	// The loop currently selected
/*
		Start

		PlayerPrefs are checked and the component is updated accordingly.
*/
		void Start() {
			
			if(PlayerPrefs.HasKey("loop_index")) {
				buttonIndex = PlayerPrefs.GetInt("loop_index");
				avatarSample.Play(PlayerPrefs.GetString("loop_name"));
			}

			loopButtons[buttonIndex].image.color = hiliteColor;
		}
/*
		ChangeLoop

		UI is updated with the new loop.

		Params
		- loopIndex(int): the index of the new loop.
*/
		public void ChangeLoop(int loopIndex) {

			loopButtons[buttonIndex].image.color = buttonColor;
			buttonIndex = loopIndex;
			loopButtons[buttonIndex].image.color = hiliteColor;

			avatarSample.CrossFadeInFixedTime(loopNames[loopIndex], 0.5f);
		}
/*
		Goto

		Loop data is saved and the next scene is loaded.

		Params
		- sceneName(string): the name of the next scene to load.
*/
		public void Goto(string sceneName) {

			PlayerPrefs.SetInt("loop_index", buttonIndex);
			PlayerPrefs.SetString("loop_name", loopNames[buttonIndex]);
			
			if(Loader.instance != null)
				Loader.instance.Load(sceneName);
		}
	}
}
