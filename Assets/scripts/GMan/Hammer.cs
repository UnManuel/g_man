// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Hammer : MonoBehaviour {

		public Weapon weapon;
		public GameObject inner;
		public Anchor anchor;

		const int STATE_ATTACK = 0;
		const int STATE_BREAK = 1;
		const int STATE_RETURN = 2;
		const int STATE_REST = 3;

		public float maxDistance = 10f;

		public float attackTime = 0.5f;
		public float breakTime = 0.3f;
		public float returnTime = 0.4f;
		public float restTime = 0.5f;

		public AnimationCurve ease;

		int state;

		float time = 0, maxTime;

		Vector3 startPosition, endPosition;

	    void OnEnable() {
	    	
	    	state = STATE_ATTACK;
	    	
	    	time = 0;
	    	maxTime = attackTime;
	    	
	    	startPosition = transform.position = anchor.transform.position = weapon.transform.position;
	    	endPosition = startPosition + Camera.main.transform.forward * maxDistance;

	    	transform.LookAt(endPosition);
	    }

	    void FixedUpdate() {
			switch(state) {

	    		case STATE_ATTACK:

			    	if(time < maxTime) {

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
			    	}

			    	break;

			    case STATE_BREAK:

			    	if(time < maxTime) {

			    		time += Time.fixedDeltaTime;

			    		if(time > maxTime) {

			    			startPosition = anchor.transform.position;

			    			time = 0;
			    			maxTime = returnTime;
			    			
			    			state = STATE_RETURN;
			    		}
			    	}

			    	transform.LookAt(Camera.main.transform.position);

			    	break;

			    case STATE_RETURN:

			    	if(time < maxTime) {

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
			    	}

			    	break;

			    default:
				    		
				    anchor.transform.position = weapon.transform.position;

			    	time += Time.fixedDeltaTime;

		    		if(time > maxTime)
		    			weapon.StoreBullet();

			    	transform.LookAt(Camera.main.transform.position);

			    	break;
	    	}
	    }
	}
}
