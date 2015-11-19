﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lib for List<GameObject>
using UnityEngine.UI;

/**
* @author HugoLS
* @version 1.0
**/
public abstract class NPC : Unit {

	public enum Blocking {
		FREE,
		SEMIBLOCK,
		BLOCK,
	};

	public enum RangeClass {
		CAC,
		LONGRANGE
	};

	float attackSpeed;
	float lastAttack;
	float soundSpeed = 1;
	float xpGain;
	protected float offsetAttackTime = 0.0f;

	float aggroDistance;
	protected float attackRange;
	float distanceToDisappear;
	float firstBlockingTime = 0.0f;
	Blocking blocking;
	protected RangeClass rangeType;
	List<Hero> heros;
	
	protected bool initiated = false;
	protected bool firstAttack = true;
	protected bool distanceUnderAggroDist = false;
	protected bool distanceUnderAttackDist = false;
	protected int successiveBlocked = 0;
	protected int nbAttack = 0;

	protected GameObject triggerAggroPrefab;
	protected GameObject triggerAttackPrefab;
	protected GameObject triggerAggro;
	protected GameObject triggerAttack;
	protected Vector3 nextAttackCoords;

	Image lifeImageNPC;

	AudioSource audio;
	protected bool audioInit = false;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	protected void Update () {
		if(initiated == false)
		{
			initNPC();
		}
		Act();
	}

	/**
	* FR:
	* Constructeur de la classe NPC
	* Les paramètres hp, damage, movementSpeed, attackType et name sont transmis lors de la construction à la classe mère (Unit) de NPC.
	* @see Unit
	*
	* EN:
	* NPC class constructor
	* Parameters hp, damage, movementSpeed, attackType and name are sent to the mother class (Unit) during the build of an NPC
	* @see Unit
	* @version 1.0
	**/
	public NPC(float aggroDistance, float attackRange, float distanceToDisappear, float attackSpeed, float xpGain, Blocking blocking, float hp, float damage, float movementSpeed, string attackType, string name)
	:base(hp, damage, movementSpeed, attackType, name){
		AttackSpeed = attackSpeed;
		XpGain = xpGain;
		this.aggroDistance = aggroDistance;
		this.attackRange = attackRange;
		this.distanceToDisappear = distanceToDisappear;
		this.blocking = blocking;
		
		if(attackType == "CaC")
		{
			rangeType = RangeClass.CAC;
		}
		else
		{
			rangeType = RangeClass.LONGRANGE;
		}
	}

	/**
	* FR:
	*	Permet à l'objet de déterminer la tâche qu'il a à faire. Attaquer, courir...
	* EN:
	*	Allows the object to know which task he has to do. Attack, run...
	* @param character
	* FR:
	*	Position du personnage sur qui effectuer l'action
	* EN:
	*	Position of the character target
	* @param deltaTime
	* FR:
	*	Temps passé depuis la dernière frame calculé
	* EN:
	*	Time passed for the last frame processed
	* @return Return an Act object
	* @version 1.0
	**/
	public void Act()
	{
		heros = GameModel.HerosInGame;
		int hero_target_index = Random.Range(0, heros.Count);
		
		Hero target = heros[hero_target_index];
		Transform character = target.transform;
		
		Vector3 position = GetPosition();
		float distance_to_hero = position.z - character.position.z;

		if(position.z < character.position.z - distanceToDisappear)
		{
			Disappear();
		}
		else if(distanceUnderAttackDist) 
		{
			//Debug.Log("Mode attack: " + name + ":"+distanceUnderAttackDist);
			UnderAttackRange(target);
		}
		else if(distanceUnderAggroDist)
		{
			//Debug.Log("Mode aggro: " + name + ":"+distanceUnderAggroDist);
			UnderAggroDistance(target);
		}
	}

	void initNPC()
	{
		triggerAggroPrefab = Resources.Load("prefabs/npc/TriggerAggro") as GameObject;
		triggerAggro = Instantiate(triggerAggroPrefab) as GameObject;
		triggerAggro.transform.parent = transform;
		triggerAggro.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-aggroDistance);

		triggerAttackPrefab = Resources.Load("prefabs/npc/TriggerAttack") as GameObject;
		triggerAttack = Instantiate(triggerAttackPrefab) as GameObject;
		triggerAttack.transform.parent = transform;
		triggerAttack.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-attackRange);

		if(GetComponentInChildren<AudioSource>())
		{
			audio = GetComponentInChildren<AudioSource>();
			audioInit = true;
		}

		lifeImageNPC = this.gameObject.GetComponentInChildren<Image> ();

		initiated = true;
	}

	public virtual void UnderAttackRange(Hero target)
	{
		if(BlockingType != Blocking.FREE)
		{
			/*if(!target.RunBlocked && firstBlockingTime == 0.0f)
			{
			*/	firstBlockingTime = Time.time;
				target.RunBlocked = true;/*
			}
			else if(firstBlockingTime + 5.0f < Time.time)
			{
				target.RunBlocked = false;	
			}*/
		}
		else
		{
			Run(Time.deltaTime);
		}
		Attack(target);
	}

	public virtual void UnderAggroDistance(Hero target)
	{
		Run(Time.deltaTime);
	}

	/**
	* FR:
	* Getter/Setter de attackSpeed
	* EN:
	* Getter/Setter of attackSpeed
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float AttackSpeed {
		get {
			return this.attackSpeed;
		}
		set {
			attackSpeed = value;
		}
	}

	
	/**
	* FR:
	* Getter/Setter de distanceUnderAggroDist
	* EN:
	* Getter/Setter of distanceUnderAggroDist
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	public bool DistanceUnderAggroDist {
		get {
			return this.distanceUnderAggroDist;
		}
		set {
			distanceUnderAggroDist = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de distanceUnderAttackDist
	* EN:
	* Getter/Setter of distanceUnderAttackDist
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	public bool DistanceUnderAttackDist {
		get {
			return this.distanceUnderAttackDist;
		}
		set {
			distanceUnderAttackDist = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de currentAttackSpeed
	* EN:
	* Getter/Setter of currentAttackSpeed
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float CurrentAttackSpeed {
		get {
			float factor = AudioManager.songAmplitude * (EnnemyConfigurator.maxMusicSpeedFactor - EnnemyConfigurator.minMusicSpeedFactor + 1) - EnnemyConfigurator.minMusicSpeedFactor;
			return (AttackSpeed / factor);
		}
	}

	/**
	* FR:
	* Getter/Setter de lastAttack
	* EN:
	* Getter/Setter of lastAttack
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float LastAttack {
		get {
			return this.lastAttack;
		}
		set {
			lastAttack = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de xpGain
	* EN:
	* Getter/Setter of xpGain
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float XpGain {
		get {
			return this.xpGain;
		}
		set {
			xpGain = value;
		}
	}

	/**
	* FR:
	* Fonction permettant de déclencher l'action: faire attaquer le NPC
	* EN:
	* Allows us to start the act: Do attack the NPC
	* @return Return a void
	* @version 1.0
	**/
	public virtual void Attack(Hero target)
	{
		//nextAttackCoords = vectorToTarget;
		if(NbAttack == 0)
		{
			if(LastAttack + CurrentAttackSpeed + offsetAttackTime < Time.time )
			{
				GetComponentInChildren<Animation>().CrossFadeQueued("Attack",0.2f);
				PlayAttackSound();
				NbAttack = NbAttack+1;
				LastAttack = Time.time;
			}
		}
		else if(LastAttack + CurrentAttackSpeed < Time.time )
		{
			GetComponentInChildren<Animation>().CrossFadeQueued("Attack",0.2f);
			PlayAttackSound();
			NbAttack = NbAttack+1;
			LastAttack = Time.time;
		}
	}

	/**
	* FR:
	* Getter/Setter de blocking
	* EN:
	* Getter/Setter of blocking
	* @return 
	* FR:
	*	Retourne un enum pour le getter et void pour le setter
	* EN:
	*	Return an enum for the getter and void for the setter
	* @version 1.0
	**/
	public Blocking BlockingType{
		get {
			return this.blocking;
		}
		set {
			blocking = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de rangeType
	* EN:
	* Getter/Setter of rangeType
	* @return 
	* FR:
	*	Retourne un enum pour le getter et void pour le setter
	* EN:
	*	Return an enum for the getter and void for the setter
	* @version 1.0
	**/
	public RangeClass RangeType{
		get {
			return this.rangeType;
		}
		set {
			rangeType = value;
		}
	}

	/**
	* FR:
	* Fonction permettant de déclencher l'action: faire disparaitre le NPC
	* EN:
	* Allows us to start the act: Do disappear the NPC
	* @return Return a void
	* @version 1.0
	**/
	public void Disappear()
	{
		base.Die();
	}

	/**
	* {@inheritDoc}
	**/
	public override void Run(float deltaTime)
	{
		transform.Translate(base.MovementSpeed * (-Vector3.forward) * deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider hit)
	{
		
		if(hit.gameObject.tag == "hero_weapon")
		{
			Hero hero = hit.GetComponent<HeroLinkWeapon>().Hero;
			LostHP(hero.Damage);
			
			Vector3 imageScale = lifeImageNPC.rectTransform.localScale;
			imageScale.Set(hp / maxHp, 1, 0);
			lifeImageNPC.rectTransform.localScale = imageScale;
			
			if(IsDead())
			{
				hero.HasKilled(XpGain);
				hero.RunBlocked = false;
				Die();
			}
		}
		else if(hit.gameObject.tag == "hero_projectile")
		{
			Hero hero = hit.GetComponent<HeroLinkWeapon>().Hero;
			LostHP(hero.Damage);
			
			Vector3 imageScale = lifeImageNPC.rectTransform.localScale;
			imageScale.Set(hp / maxHp, 1, 0);
			lifeImageNPC.rectTransform.localScale = imageScale;
			
			if(IsDead())
			{
				hero.HasKilled(XpGain);
				hero.RunBlocked = false;
				Die();
			}
			
			//fireball collides with an ennemy. Destruct it !
			Destroy(hit.gameObject);
		}
	}

	public int SuccessiveBlocked{
		get {
			return this.successiveBlocked;
		}
		set {
			successiveBlocked = value;
		}
	}

	public int NbAttack{
		get {
			return this.nbAttack;
		}
		set {
			nbAttack = value;
		}
	}

	public void Blocked()
	{
		//Debug.LogWarning("Allez blockéééé");
		SuccessiveBlocked += 1;
		//Debug.LogWarning(SuccessiveBlocked);
	}

	void OnDestroy(){
		GameModel.NPCsInGame.Remove (this);
	}

	public void PlayAttackSound(){
		if(audioInit)
		{
			if(audio.isPlaying){
				audio.Stop();
			}
			audio.Play();
		}
	}


}
