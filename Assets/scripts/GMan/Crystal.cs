// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Crystal

	A crystal that attracts nearby satellite objects.
*/
	public class Crystal : MonoBehaviour {

		public Orbit[] orbits;    	// Three paths shaped like an atom

		int currentOrbitIndex = 0;	// The orbit being evaluated to add a satellite
/*
		OnTriggerStay

		Checks if the satellite inside of the crystal's collider
		can be added to one of the orbits (current capacity is 24).
*/
		void OnTriggerStay(Collider c) {

			Satellite sat = c.GetComponent<Satellite>();

			if(sat != null && sat.anchor == null) {
				
				orbits[currentOrbitIndex].Catch(sat);

				if(++currentOrbitIndex == orbits.Length)
					currentOrbitIndex = 0;
			}
		}
	}
}