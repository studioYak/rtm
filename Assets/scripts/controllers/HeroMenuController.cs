using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroMenuController : MonoBehaviour {

	string userName;

	InputField inputName;

	Button buttonPlay;
	Button buttonWarrior;
	Button buttonWizard;
	Button buttonMonk;

	bool warrior, wizard, monk;

	ColorBlock cb;

	// Use this for initialization
	void Start () {
		inputName = GameObject.Find("InputName").GetComponent<InputField>();

		buttonPlay = GameObject.Find("Play").GetComponent<Button>();
		buttonWarrior = GameObject.Find("Warrior").GetComponent<Button>();
		buttonWizard = GameObject.Find("Wizard").GetComponent<Button>();
		buttonMonk = GameObject.Find("Monk").GetComponent<Button>();

		buttonPlay.interactable = false;

		cb = buttonWarrior.colors;

		userName = "";

		warrior = false;
		wizard = false;
		monk = false;
	}
	
	// Update is called once per frame
	void Update () {
		userName = inputName.text;
		if(((warrior|wizard|monk) == true) & userName != ""){
			buttonPlay.interactable = true;
		} else buttonPlay.interactable = false;

	}

	public void Warrior(){
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonWarrior.colors = cb;
		warrior = true;
		
		cb.normalColor = Color.white;
		buttonWizard.colors = cb;
		buttonMonk.colors = cb;
		wizard = false;
		monk = false;

	}

	public void Wizard() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonWizard.colors = cb;
		wizard = true;

		cb.normalColor = Color.white;
		buttonWarrior.colors = cb;
		buttonMonk.colors = cb;
		warrior = false;
		monk = false;
	}

	public void Monk() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonMonk.colors = cb;
		monk = true;

		cb.normalColor = Color.white;
		buttonWarrior.colors = cb;
		buttonWizard.colors = cb;
		warrior = false;
		wizard = false;

	}

	public void Play(){
		if (warrior) GameModel.Hero = new Warrior();
		if (monk) GameModel.Hero = new Monk();
		if (wizard) GameModel.Hero = new Wizard();
		GameModel.Hero.Name = userName;
		Application.LoadLevel("GameScene");
	}

	public void Return() {
		Application.LoadLevel("Main_menu");
	}
}
