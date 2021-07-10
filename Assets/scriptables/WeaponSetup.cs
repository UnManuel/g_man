// by @unmanuel

using UnityEngine;
/*
	WeaponSetup

	Weapon configuration is stored here.
*/
[CreateAssetMenu(fileName = "weapon_setup_", menuName = "Scriptables/WeaponSetup", order = 1)]
public class WeaponSetup : ScriptableObject {
	
	public int bulletCount = 1;     	// Bullet rounds
	public int bucketSize = 1;      	// Number of bullet clones to be internally created
	public float maxCooldown = 0.5f;	// Duration between shots
}
