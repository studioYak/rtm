using UnityEngine;
using System.Collections;

public class Save {

	private Hero hero;

	private Level level;

	public Hero Hero {
		get {
			return hero;
		}
		
		set {
			hero = value;
		}
	}

	public Level Level {
		get {
			return level;
		}
		
		set {
			level = value;
		}
	}

}
