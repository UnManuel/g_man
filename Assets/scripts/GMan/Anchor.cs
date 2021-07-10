// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour {

	public Satellite target;
	public float force = 1000;
	public float gizmoSize = 1f;
	public Color gizmoColor = Color.red;
    
    void OnDrawGizmos() {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    void FixedUpdate() {
    	if(target != null)
    		target.body.velocity = (transform.position - target.transform.position) * force * Time.fixedDeltaTime;
    }
}
