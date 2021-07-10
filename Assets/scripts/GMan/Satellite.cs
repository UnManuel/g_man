// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Satellite

	A game object that is anchored to another in order to follow set paths.
*/
	public class Satellite : MonoBehaviour {

		public Collider box;  	// Hitbox of the satellite
		public Rigidbody body;	// Rigidbody whose velocity is course-corrected
		public Anchor anchor; 	// Reference to the owning anchor
	}
}