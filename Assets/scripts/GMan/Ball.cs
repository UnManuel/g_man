// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector3 startupForce;
	public Rigidbody body;

	void OnEnable() {
		body.AddForce(Camera.main.transform.up * startupForce.y + Camera.main.transform.forward * startupForce.z);
	}
}
