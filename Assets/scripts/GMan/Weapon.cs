// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Weapon

	A general-purpose weapon which acts as an abstraction of a gun.
	Works with different configurations provided by a setup file. 
*/
	public class Weapon : MonoBehaviour {

		public WeaponSetup setup;        	// Scriptable object file
		public GameObject baseBullet;    	// Brandished by the weapon

		int bulletCount;                 	// Capacity of the weapon
		float cooldown;                  	// Time between shots
		Queue<GameObject> pool, deadPool;	// Pools to handle bullet clones
/*
		Start

		Bullets are duplicated from the base and stored for later use.
*/
		void Start() {
			if(setup.bucketSize > 0) {

				pool = new Queue<GameObject>();
				deadPool = new Queue<GameObject>();

				deadPool.Enqueue(baseBullet);

				for(int i = 1; i < setup.bucketSize; ++i)
					deadPool.Enqueue(Instantiate(baseBullet, baseBullet.transform.parent));
			}
		}
/*
		OnEnable

		Weapon is being readied to shoot again.
*/
		void OnEnable() {
			bulletCount = setup.bulletCount;
			baseBullet.SetActive(false);
		}
/*
		Update

		A barrage of bullets is released if available. If the bullet pool
		is empty, it uses the base bullet as a single weapon instead.
*/
		void Update() {

			if(Input.GetMouseButtonDown(0)) {

				if(setup.bucketSize == 0)
					baseBullet.SetActive(true);

				cooldown = 0;
			}

			if(Input.GetMouseButton(0) && bulletCount != 0) {

				cooldown -= Time.deltaTime;

				if(cooldown < 0)
					cooldown = 0;

				if(cooldown == 0) {

					GameObject bullet = null;

					if(deadPool.Count > 0) {
						bullet = deadPool.Dequeue();
						bullet.transform.position = transform.position;
						bullet.SetActive(true);
					} else {
						bullet = pool.Dequeue();
						bullet.transform.position = transform.position;
						bullet.SendMessage("OnEnable", null, SendMessageOptions.DontRequireReceiver);
					}

					pool.Enqueue(bullet);

					if(bulletCount > 0)
						--bulletCount;

					cooldown = setup.maxCooldown;
				}
			}

			if(Input.GetMouseButtonUp(0) && setup.bucketSize == 0)
				baseBullet.SetActive(false);
		}
/*
		StoreBullet

		Makes the weapon store a bullet that is already spent.
*/
		public void StoreBullet() {
			if(pool.Count > 0) {

				++bulletCount;
				
				GameObject bullet = pool.Dequeue();

				bullet.SetActive(false);
				
				deadPool.Enqueue(bullet);
			}
		}
	}
}
