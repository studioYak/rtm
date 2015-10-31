using UnityEngine;
using System.Collections;

public class HeroConfigurator : MonoBehaviour {

	// HERO
	/*public static string ;
	public static float ;*/

	// WARRIOR
	public static string warriorAttackType = "CaC";
	public static float warriorXpQuantity = 0.0f;
	public static float warriorBlockingPercent = 100.0f;
	public static float warriorPowerQuantity = 1000.0f;
	public static float warriorHpRefresh = 2.0f;
	public static float warriorPowerRefresh = 0.0f;
	public static float warriorHp = 1000.0f;
	public static float warriorDamage = 10.0f;
	public static float warriorMovementSpeed = 3.0f;
	public static float warriorRange = 5.0f;

	// MONK
	public static string monkAttackType = "CaC";
	public static float monkXpQuantity = 0.0f;
	public static float monkBlockingPercent = 50.0f;
	public static float monkPowerQuantity = 1000.0f;
	public static float monkHpRefresh = 4.0f;
	public static float monkPowerRefresh = 0.0f;
	public static float monkHp = 1100.0f;
	public static float monkDamage = 8.0f;
	public static float monkMovementSpeed = 3.0f;
	public static float monkRange = 5.0f;

	// WIZARD
	public static string wizardAttackType = "Distance";
	public static float wizardXpQuantity = 0.0f;
	public static float wizardBlockingPercent = 100.0f;
	public static float wizardPowerQuantity = 1000.0f;
	public static float wizardHpRefresh = 1.0f;
	public static float wizardPowerRefresh = 0.0f;
	public static float wizardHp = 1100.0f;
	public static float wizardDamage = 15.0f;
	public static float wizardMovementSpeed = 3.0f;
	public static float wizardRange = 5.0f;


	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	public void Init()
	{
		// Lire dans le fichier JSON

		// Hydrater les variables
	}
}
