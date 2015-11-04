using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {

	float damage = 0.0f;

	public void Init(float weapon_damage)
	{
		damage = weapon_damage;
	}


	void OnTriggerEnter(Collider hit)
	{
		if(hit.transform.tag == "hero_shield")
		{
			hit.transform.root.SendMessage("DefenseMode","on");
			hit.transform.root.SendMessage("LostHP",damage);
		}
		else if(hit.transform.tag == "hero")
		{
			hit.transform.root.SendMessage("LostHP",damage);
		}
	}
}