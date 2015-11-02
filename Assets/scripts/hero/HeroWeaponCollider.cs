using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class HeroWeaponCollider : MonoBehaviour {

	float damage = 10000.0f;

/*	public void Init(float weapon_damage)
	{
		damage = weapon_damage;
	}
*/

	void OnTriggerEnter(Collider hit)
	{
		Debug.LogWarning("Collision arme");
		if(hit.gameObject.tag == "ennemy")
		{
			Debug.LogWarning("Collision avec ennemy");
			hit.gameObject.SendMessage("LostHP",damage);
		}
	}
}
