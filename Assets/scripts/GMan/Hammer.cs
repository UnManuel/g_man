// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Hammer

	It works just like Thor's Mjolnir. Nuff Said!
*/
	public class Hammer : MonoBehaviour {

		const int STATE_ATTACK = 0;        		// Hammer is moving forward
		const int STATE_BREAK = 1;         		// Hammer waits before returning
		const int STATE_RETURN = 2;        		// Hammer is returning
		const int STATE_REST = 3;          		// Hammer rests before vanishing

		public Weapon weapon;              		// Owning weapon
		public GameObject inner;           		// The hammer object being launched
		public Anchor anchor;              		// The anchor followed by the hammer

		public float maxDistance = 10f;    		// As far as the hammer goes

		public float attackTime = 0.5f;    		// Duration of attack state
		public float breakTime = 0.3f;     		// Duration of break state
		public float returnTime = 0.4f;    		// Duration of returning state
		public float restTime = 0.5f;      		// Duration of resting state

		public AnimationCurve ease;        		// Easing used at return

		int state;                         		// Current state

		float time = 0, maxTime;           		// State clock

		Vector3 startPosition, endPosition;		// Endpoints of the flying hammer
/*
		OnEnable

		The hammer is prepared to be launched forwards.
*/
		void OnEnable() {

			state = STATE_ATTACK;
			
			time = 0;
			maxTime = attackTime;
			
			startPosition = transform.position = anchor.transform.position = weapon.transform.position;
			endPosition = startPosition + Camera.main.transform.forward * maxDistance;

			transform.LookAt(endPosition);
		}
/*
		FixedUpdate

		The core of the state machine. Each state deals with
		the anchor being moved and the hammer following suit. 
*/
		void FixedUpdate() {
			switch(state) {

				case STATE_ATTACK:

					time += Time.fixedDeltaTime;

					if(time > maxTime)
						time = maxTime;

					anchor.transform.position = Vector3.Lerp(startPosition, endPosition, time / maxTime);
					inner.transform.localEulerAngles = new Vector3(time / maxTime * 1080f + 90f, 0, 0);

					if(time == maxTime) {

						time = 0;
						maxTime = breakTime;

						state = STATE_BREAK;
					}

					break;

				case STATE_BREAK:

					time += Time.fixedDeltaTime;

					if(time > maxTime) {

						startPosition = anchor.transform.position;

						time = 0;
						maxTime = returnTime;
						
						state = STATE_RETURN;
					}

					transform.LookAt(Camera.main.transform.position);

					break;

				case STATE_RETURN:

					time += Time.fixedDeltaTime;

					if(time > maxTime)
						time = maxTime;
					
					endPosition = weapon.transform.position;

					anchor.transform.position = Vector3.Lerp(startPosition, endPosition, ease.Evaluate(time / maxTime));

					transform.LookAt(Camera.main.transform.position);

					if(time == maxTime) {
					
						time = 0;
						maxTime = restTime;

						state = STATE_REST;
					}

					break;

				default:

					time += Time.fixedDeltaTime;

					if(time > maxTime)
						weapon.StoreBullet();
							
					anchor.transform.position = weapon.transform.position;

					transform.LookAt(Camera.main.transform.position);

					break;
			}
		}
	}
}
