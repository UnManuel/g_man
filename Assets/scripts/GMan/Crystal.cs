// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Crystal : MonoBehaviour {

		public Orbit[] orbits;

		int currentOrbitIndex = 0;

		void OnDisable() {
			for(int i = 0; i < orbits.Length; ++i)
				orbits[i].ReleaseSatellites();
		}

		void OnTriggerEnter(Collider c) {

			Satellite sat = c.GetComponent<Satellite>();

			if(sat != null && sat.anchor == null) {
				
				orbits[currentOrbitIndex].Catch(sat);

				if(++currentOrbitIndex == orbits.Length)
					currentOrbitIndex = 0;
			}
		}
	}
}