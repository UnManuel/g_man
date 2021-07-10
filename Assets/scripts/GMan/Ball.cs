// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Ball

	A bouncing ball that is launched with the provided force.
*/
	public class Ball : MonoBehaviour {

		public Vector3 startupForce;	// The force applied when launched
		public Rigidbody body;      	// Body of the ball
/*
		OnEnable

		Applies the force upon activation.
*/
		void OnEnable() {
			body.AddForce(Camera.main.transform.up * startupForce.y + Camera.main.transform.forward * startupForce.z);
		}
	}
}
