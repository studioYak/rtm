using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>

/**
* @author HugoLS
* @version 1.0
**/
public class EnnemyWeaponCollider : MonoBehaviour {


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
		}
	}
}