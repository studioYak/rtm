using UnityEngine;
using System.Collections;

public class Tutorial {

	private string text;
	private string imagePath;

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
}
