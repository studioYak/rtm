using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadMenuController : MonoBehaviour {

	Button buttonPlay;
	Button buttonSlot1;
	Button buttonSlot2;
	Button buttonSlot3;

	List<Button> buttons;
	List<bool> slots;

	ColorBlock cb;

	bool slot1, slot2, slot3;

	// Use this for initialization
	void Start () {
		buttonPlay = GameObject.Find("Play").GetComponent<Button>();
		buttonSlot1 = GameObject.Find("ButtonSlot1").GetComponent<Button>();
		buttonSlot2 = GameObject.Find("ButtonSlot2").GetComponent<Button>();
		buttonSlot3 = GameObject.Find("ButtonSlot3").GetComponent<Button>();

		cb = buttonSlot1.colors;

		buttonPlay.interactable = false;
		buttonSlot1.interactable = false;
		buttonSlot2.interactable = false;
		buttonSlot3.interactable = false;

		slot1 = false;
		slot2 = false;
		slot3 = false;

		buttons  = new List<Button>();
		//buttons.Add(buttonSlot1);
		//buttons.Add(buttonSlot2);
		//buttons.Add(buttonSlot3);

		slots = new List<bool>();
		//slots.Add (buttonSlot1.interactable);
		//slots.Add (buttonSlot2.interactable);
		//slots.Add (buttonSlot3.interactable);

		checkSave();
		showLevel();

	}
	
	// Update is called once per frame
	void Update () {
		if ((slot1 | slot2 | slot3) == true) {
			buttonPlay.interactable = true;
		}
	}

	void checkSave() {

	}

	void showLevel() {
		int i;
		string s = " à moi";
		for (i=0; i<buttons.Count; i++){
			Debug.Log(slots[i]);
			if(slots[i]){
				buttons[i].GetComponentInChildren<Text>().text = "maman\n"+s;
			}
		}
	}

	public void Slot1 () {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot1.colors = cb;
		slot1 = true;
		
		cb.normalColor = Color.white;
		buttonSlot2.colors = cb;
		buttonSlot3.colors = cb;
		slot2 = false;
		slot3 = false;
	}

	public void Slot2 () {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot2.colors = cb;
		slot2 = true;
		
		cb.normalColor = Color.white;
		buttonSlot1.colors = cb;
		buttonSlot3.colors = cb;
		slot1 = false;
		slot3 = false;
	}

	public void Slot3() {
		cb.normalColor = new Color32(163, 124, 124, 255);
		buttonSlot3.colors = cb;
		slot3 = true;
		
		cb.normalColor = Color.white;
		buttonSlot1.colors = cb;
		buttonSlot2.colors = cb;
		slot1 = false;
		slot2 = false;
	}

	public void Play() {
		Application.LoadLevel ("GameScene");
	}

	public void Return() {
		Application.LoadLevel ("Main_menu");
	}

}
