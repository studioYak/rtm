using UnityEngine;
using System.Collections;

public class Tutorial {

	private string text;
	private string imagePath;
	private bool played;
	private string name;

	public Tutorial(string text, string imagePath, string name) {

	}

	public string Text {
		get {
			return this.text;
		}

		set {
			this.text = value;
		}
	}

	public string ImagePath {
		get {
			return this.imagePath;
		}
		
		set {
			this.imagePath = value;
		}
	}

	public bool Played {
		get {
			return this.played;
		}

		set {
			this.played = value;
		}
	}


}
