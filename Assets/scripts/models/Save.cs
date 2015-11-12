using UnityEngine;
using System.Collections;

public class Save {

	private Hero hero;

	private int levelId;

	public Save(Hero hero, int levelId){
		this.hero = hero;
		this.levelId = levelId;
	}

	public Save(){
	}

	public Hero Hero {
		get {
			return hero;
		}
		
		set {
			hero = value;
		}
	}

	public int LevelId {
		get {
			return levelId;
		}
		
		set {
			levelId = value;
		}
	}

}
