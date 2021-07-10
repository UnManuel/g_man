// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GMan {

	public class Item : MonoBehaviour {

		public SpriteRenderer glow;
		public bool faceCamera = false;
		public float offset = 1f;
		public float floatSpeed = 1f;
		public float spinDegreesPerSecond = 90f;
		public AnimationCurve ease;
		public UnityEvent OnGet;

		float time = 0;

		Vector3 pivot;

		void Start() {
			time = Random.Range(0f, 5f);
			pivot = transform.position;
		}

		void Update() {

			time += Time.deltaTime;

			transform.position = pivot + Vector3.up * ease.Evaluate(Mathf.PingPong(time * floatSpeed, 1f)) * offset;
			transform.Rotate(Vector3.up * spinDegreesPerSecond * Time.deltaTime);

			if(faceCamera)
				transform.LookAt(Camera.main.transform.position);

			if(glow != null)
				glow.gameObject.transform.LookAt(Camera.main.transform.position);
		}

		void OnTriggerEnter(Collider c) {
			if(c.GetComponent<Avatar>() != null) {
				gameObject.SetActive(false);
				OnGet?.Invoke();
			}
		}
	}
}
