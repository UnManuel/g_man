// by @unmanuel

using UnityEngine;
/*
	WeaponSetup

	Weapon config is stored here.
*/
[CreateAssetMenu(fileName = "weapon_setup_", menuName = "Scriptables/WeaponSetup", order = 1)]
public class WeaponSetup : ScriptableObject {
	
	public int bulletCount = 1;
	public int bucketSize = 1;
	public float maxCooldown = 0.5f;
}
