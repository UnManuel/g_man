// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Orbit : MonoBehaviour {

		public Spline path;

		public Anchor[] anchors;

		public float maxOrbitTime = 10;

		float orbitTime = 0;

		float[] offsets;

		int nextAnchorIndex;

		void OnEnable() {
		}

		void Start() {

			offsets = new float[anchors.Length];

			float distance = 1f / offsets.Length;

			float offset = 0;

			for(int i = 0; i < offsets.Length; ++i) {
				offsets[i] = offset;
				offset += distance;
			}
		}

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

		public void Catch(Satellite sat) {
			if(nextAnchorIndex < anchors.Length) {
				anchors[nextAnchorIndex].target = sat;	
				++nextAnchorIndex;
			}
		}

		public void ReleaseSatellites() {

			for(int i = 0; i < nextAnchorIndex; ++i)
				anchors[i].target = null;

			nextAnchorIndex = 0;
		}
	}
}
