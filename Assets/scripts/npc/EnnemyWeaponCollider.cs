using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {

<<<<<<< HEAD
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
=======

	void OnTriggerEnter(Collider hit)
	{
		if(hit.transform.tag == "hero_defense")
		{
			Debug.Log ("DEFENSE");
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("on");
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if(hit.transform.tag == "hero_defense")
		{
			Hero hero = hit.gameObject.GetComponentInParent<Hero>();
			hero.DefenseMode("off");
>>>>>>> 0ae1df22b18c20115bcfe65fec22b7105eddb16f
		}
	}
}