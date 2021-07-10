// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GMan {
/*
	Item

	An item on the scene to grab and make something happen.
*/
	public class Item : MonoBehaviour {

		public SpriteRenderer glow;             	// A glowing sprite
		public bool faceCamera = false;         	// Makes the item face the player
		public float offset = 1f;               	// Vertical offset when hovering
		public float floatSpeed = 1f;           	// How fast the item floats
		public float spinDegreesPerSecond = 90f;	// Horizontal spin speed
		public AnimationCurve ease;             	// Easing used by the hovering
		public UnityEvent OnGet;                	// Called when getting the item

		float time = 0;                         	// Internal clock

		Vector3 pivot;                          	// Startup position
/*
		Start

		Hovering values are initialized.
*/
		void Start() {
			time = Random.Range(0f, 5f);
			pivot = transform.position;
		}
/*
		Update

		The item hovers and either spins or faces the camera.
*/
		void Update() {

			time += Time.deltaTime;

			transform.position = pivot + Vector3.up * ease.Evaluate(Mathf.PingPong(time * floatSpeed, 1f)) * offset;
			transform.Rotate(Vector3.up * spinDegreesPerSecond * Time.deltaTime);

			if(faceCamera)
				transform.LookAt(Camera.main.transform.position);

			if(glow != null)
				glow.gameObject.transform.LookAt(Camera.main.transform.position);
		}
/*
		OnTriggerEnter

		The item triggers its designated event and vanishes.
*/
		void OnTriggerEnter(Collider c) {
			if(c.GetComponent<Avatar>() != null)
				OnGet?.Invoke();
		}
	}
}
