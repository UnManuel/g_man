// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMan {
/*
	Avatar

	The player's presence inside of the scene.
*/
	public class Avatar : MonoBehaviour {

		public CharacterController controller;	// We move the player with this

		public Weapon[] weapons;              	// Available weapons

		public float speed = 10;              	// Movement speed
		public int currentWeaponIndex = -1;   	// -1 if no weapon is equipped
/*
		Start

		Equips a weapon if the current index allows for it.
*/
		void Start() {
			if(currentWeaponIndex != -1)
				ChangeWeapon(currentWeaponIndex);
		}
/*
		FixedUpdate

		Moves the controller according with the user's input.
*/
		void FixedUpdate() {
			Vector3 direction = Camera.main.transform.forward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
			controller.Move(new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime);
		}
/*
		ChangeWeapon

		Turns off the current weapon and equips the one with the provided index.

		Params
		- weaponIndex(int): the index inside the weapons array.
*/
		public void ChangeWeapon(int weaponIndex) {
			if(weaponIndex != currentWeaponIndex) {

				if(currentWeaponIndex != -1)
					weapons[currentWeaponIndex].gameObject.SetActive(false);
				
				currentWeaponIndex = weaponIndex;
				weapons[weaponIndex].gameObject.SetActive(true);
			}
		}
	}
}
