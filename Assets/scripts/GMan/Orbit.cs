// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Orbit

	A spline holding several anchors moving through it.
*/
	public class Orbit : MonoBehaviour {

		public Spline path;            		// The aforementioned spline

		public Anchor[] anchors;       		// The anchors being moved

		public float maxOrbitTime = 10;		// Time to complete one full orbit

		float orbitTime = 0;           		// Current orbit time

		float[] offsets;               		// Distance between anchors

		int nextAnchorIndex;           		// Current free anchor slot
/*
		Start

		Anchor positions are calculated alongside the path.
*/
		void Start() {

			offsets = new float[anchors.Length];

			float distance = 1f / offsets.Length;

			float offset = 0;

			for(int i = 0; i < offsets.Length; ++i) {
				offsets[i] = offset;
				offset += distance;
			}
		}
/*
		OnDisable

		Anchor contents are released.
*/
		void OnDisable() {

			for(int i = 0; i < nextAnchorIndex; ++i) {
				anchors[i].target.anchor = null;
				anchors[i].target = null;
			}

			nextAnchorIndex = 0;
		}
/*
		FixedUpdate

		Anchors are updated inside of the spline.
*/
		void FixedUpdate() {

			orbitTime += Time.fixedDeltaTime;

			if(orbitTime > maxOrbitTime)
				orbitTime -= maxOrbitTime;

			float time = orbitTime / maxOrbitTime;

			float offset;

			for(int i = 0; i < anchors.Length; ++i) {

				offset = time + offsets[i];

				if(offset > 1f)
					offset -= 1f;

				iTween.PutOnPath(anchors[i].gameObject, path.controlPoints, offset);
			}
		}
/*
		Catch

		A new satellite is added to the orbit if there is a slot available.

		Params
		- sat(Satellite): The satellite being evaluated for admission.
*/
		public void Catch(Satellite sat) {
			if(nextAnchorIndex < anchors.Length) {
				sat.anchor = anchors[nextAnchorIndex];
				anchors[nextAnchorIndex].target = sat;
				++nextAnchorIndex;
			}
		}
	}
}
