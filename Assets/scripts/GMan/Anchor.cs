// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Anchor

	An object that is moved without a rigidbody, but it is followed by one.
	This makes easy to move rigidbodies through set paths.
*/
	public class Anchor : MonoBehaviour {

		public Satellite target;            	// The anchored satellite
		public float force = 1000;          	// Amount of force to push the satellite towards the anchor
		public float gizmoSize = 1f;        	// Anchor size in editor mode
		public Color gizmoColor = Color.red;	// Anchor color in editor mode
/*
		OnDrawGizmos

		Draws the anchor as a gizmo in editor mode.
*/
		void OnDrawGizmos() {
			Gizmos.color = gizmoColor;
			Gizmos.DrawWireSphere(transform.position, gizmoSize);
		}
/*
		FixedUpdate

		If there is a target then its velocity is updated to point towards the anchor.
*/
		void FixedUpdate() {
			if(target != null)
				target.body.velocity = (transform.position - target.transform.position) * force * Time.fixedDeltaTime;
		}
	}
}