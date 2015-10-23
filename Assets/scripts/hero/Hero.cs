using UnityEngine;
using System.Collections;


/**
* @author HugoLS
* @version 1.0
**/
public abstract class Hero : Unit {

	int xpQuantity;
	string handAttack;
	int powerQuantity;
	int maxPowerQuantity;
	int hpRefresh;
	int powerRefresh;
	bool defending;
	int blockingPercent;
	float range;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	* FR:
	* Constructeur de la classe Hero
	* Les paramètres hp, damage, movementSpeed, attackType et name sont transmis lors de la construction à la classe mère (Unit) de Héro.
	* @see Unit
	*
	* EN:
	* Hero class constructor
	* Parameters hp, damage, movementSpeed, attackType and name are sent to the mother class (Unit) during the build of an Hero
	* @see Unit
	*
	* @param xpQuantity
	*					FR:
	*					Quantité d'expérience courante du Héro à créer
	*					EN:
	*					Current quantity of experience of the Hero
	* @param blockingPercent
	*					FR:
	*					Taux de blocage du personnage. Une valeur de 100 correspond à un taux de blocage de 100% des dégâts d'une attaque
	*					La valeur doit donc être comprise entre 0 et 100.
	*					EN:
	*					Block rate of the character. A value of 100 match to a block damage rate of 100% for an attack.
	*					The value has to be between 0 and 100.
	* @param handAttack
	*					FR:
	*					Cette variable détermine la main "forte" du joueur. Si il est droitier, alors la valeur de handAttack sera droite.
	*					EN:
	*					This variable determine the strong hand of a player. If he is right-handed, then the value should be "right"
	* @param powerQuantity
	*					FR:
	*					Quantité de pouvoir (fureur ou mana) courante du héro. Cette valeur est obligatoirement inférieur ou égale à la quantité maximum définie
	*					EN:
	*					Current quantity of power (furor or mana) of the Hero. This value must be equal or inferior to the maximum quantity defined.
	* @param hpRefresh
	*					FR:
	*					Taux de récupération des points de vie du héro.
	*					EN:
	*					Health Point refresh rate of the Hero.
	* @param powerRefresh
	*					FR:
	*					Taux de récupération des points de puissance du héro.
	*					EN:
	*					Power refresh rate of the Hero.
	* @param defending
	*					FR:
	*					Booléen permettant de définir si le héro est actuellement en train de se défendre.
	*					EN:
	*					Boolean which permit to define if the hero is defending or not.
	* @param hp
	*					FR:
	*					Quantité de point de vie du héro
	*					EN:
	*					Health Point quantity of the hero
	* @param damage
	*					FR:
	*					Nombre de dégât que le héro inflige en attaquant
	*					EN:
	*					Damage done when the hero attack
	* @param movementSpeed
	*					FR:
	*					Vitesse de déplacement du héros. La vitesse doit être calqué sur le modèle humain, soit 1 = 1m/s parcouru. 1,3 correspond à environ 5km/h
	*					EN:
	*					Movement speed of the Hero. The speed must be according to the human model,so 1 = 1m/s walked. 1,3m/s is according to 5km/h.
	* @param attackType
	*					FR:
	*					Permet de déterminer si le héros attaque à distance ou au corps à corps
	*					EN:
	*					Set if the Hero attack remotely or hand to hand
	* @param name
	*					FR:
	*					Nom du joueur
	*					EN:
	*					Name of the player
	* @version 1.0
	**/
	public Hero(int xpQuantity,int blockingPercent, string handAttack, int powerQuantity, int hpRefresh, int powerRefresh, bool defending, int hp, int damage, int movementSpeed, string attackType, string name)
		:base(hp, 1000*damage, movementSpeed, attackType, name){
		XpQuantity = xpQuantity;
		HandAttack = handAttack;
		PowerQuantity = powerQuantity;
		MaxPowerQuantity = powerQuantity;
		HpRefresh = hpRefresh;
		PowerRefresh = powerRefresh;
		BlockingPercent = blockingPercent;
		range = 5;
	}

	/**
	* FR:
	* Getter/Setter de xpQuantity
	* EN:
	* Getter/Setter of xpQuantity
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int XpQuantity {
		get {
			return this.xpQuantity;
		}
		set {
			xpQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de range
	* EN:
	* Getter/Setter of range
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float Range {
		get {
			return this.range;
		}
		set {
			range = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de blockingPercent
	* EN:
	* Getter/Setter of blockingPercent
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int BlockingPercent {
		get {
			return this.blockingPercent;
		}
		set {
			blockingPercent = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de handAttack
	* EN:
	* Getter/Setter of handAttack
	* @return 
	* FR:
	*	Retourne une string pour le getter et void pour le setter
	* EN:
	*	Return a string for the getter and void for the setter
	* @version 1.0
	**/
	public string HandAttack {
		get {
			return this.handAttack;
		}
		set {
			handAttack = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de powerQuantity
	* EN:
	* Getter/Setter of powerQuantity
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int PowerQuantity {
		get {
			return this.powerQuantity;
		}
		set {
			powerQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de maxPowerQuantity
	* EN:
	* Getter/Setter of maxPowerQuantity
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int MaxPowerQuantity {
		get {
			return this.maxPowerQuantity;
		}
		set {
			maxPowerQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de hpRefresh
	* EN:
	* Getter/Setter of hpRefresh
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int HpRefresh {
		get {
			return this.hpRefresh;
		}
		set {
			hpRefresh = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de powerRefresh
	* EN:
	* Getter/Setter of powerRefresh
	* @return 
	* FR:
	*	Retourne un int pour le getter et void pour le setter
	* EN:
	*	Return an int for the getter and void for the setter
	* @version 1.0
	**/
	public int PowerRefresh {
		get {
			return this.powerRefresh;
		}
		set {
			powerRefresh = value;
		}
	}

	/**
	* FR:
	* Cette fonction permet de calculer et d'infliger des dégâts suite à une attaque. Les dégâts infligés dépendent de la défense ou non du héro, mais aussi de son taux de blocage de dégât.
	* EN:
	* This function permit to process and inflict damages after an ennemy attack. Damages depend if the hero is defending or not but they depend of the blocking rate.
	* @param damage
	* FR:
	*	Quantité de dégâts brute que doit infliger la fonction au héro
	* EN:
	*	Damage quantity take by the hero
	* @return Return void
	* @version 1.0
	**/
	public void LostHP(int damage)
	{
		float damageToLost = 0.0f;
		if(Defending)
		{
			damageToLost = damage - (blockingPercent*damage/100);
		}
		else
		{
			damageToLost = damage;
		}
		base.LostHP((int)damageToLost);
	}

	/**
	* FR:
	* Getter/Setter de defending
	* EN:
	* Getter/Setter of defending
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	bool Defending {
		get {
			return this.defending;
		}
		set {
			defending = value;
		}
	}

	/**
	* {@inheritDoc}
	**/
	public override void Run(float deltaTime)
	{
		transform.Translate(base.MovementSpeed * Vector3.forward * deltaTime, Space.World);
	}

	/**
	* FR:
	* Cette fonction permet de passer le héro en mode défensif ou en mode non défensif en fonction du paramètre d'entrée
	* EN:
	* Permits to switch the hero between defensive mode and no defensive mode in function of the input
	* @param mode
	* FR:
	*	Ce paramètre doit être égale à "off" pour sortir du mode défense, et à "on" pour passer en mode défense
	* EN:
	* 	This paramater have to be equal to "off" to leave the defense mode and equal to "on" to enter in defense mode
	* @return Return void
	* @version 1.0
	**/
	public void DefenseMode(string mode){
		if (mode == "off") {
			Defending = false;
		} else {
			Defending = true;
		}
	}
}
