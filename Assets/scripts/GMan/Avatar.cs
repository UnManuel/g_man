// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {

	public class Avatar : MonoBehaviour {

		public CharacterController controller;

		public Weapon[] weapons;

		public float speed = 10;
		public int currentWeaponIndex = -1;

		void Start() {
			if(currentWeaponIndex != -1)
				ChangeWeapon(currentWeaponIndex);
		}

		void FixedUpdate() {
			Vector3 direction = Camera.main.transform.forward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
			controller.Move(new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime);
		}

		public void ChangeWeapon(int weaponIndex) {
			
			if(currentWeaponIndex != -1)
				weapons[currentWeaponIndex].gameObject.SetActive(false);
			
			currentWeaponIndex = weaponIndex;
			weapons[weaponIndex].gameObject.SetActive(true);
		}
	}
}
