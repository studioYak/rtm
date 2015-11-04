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
		if(hit.transform.tag == "hero_shield")
		{
			hit.gameObject.GetComponentInParent<Hero>().DefenseMode("on");
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if(hit.transform.tag == "hero_shield")
		{
			hit.gameObject.GetComponentInParent<Hero>().DefenseMode("off");
		}
	}
}