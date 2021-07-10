// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Weapon : MonoBehaviour {

		public WeaponSetup setup;
		public GameObject baseBullet;

		int bulletCount;
		float cooldown;
		Queue<GameObject> pool, deadPool;

		void Start() {
			if(setup.bucketSize > 0) {

				pool = new Queue<GameObject>();
				deadPool = new Queue<GameObject>();

				deadPool.Enqueue(baseBullet);

				for(int i = 1; i < setup.bucketSize; ++i)
					deadPool.Enqueue(Instantiate(baseBullet, baseBullet.transform.parent));
			}
		}

		void OnEnable() {
			bulletCount = setup.bulletCount;
		}

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
						bullet.SendMessage("OnEnable");
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
