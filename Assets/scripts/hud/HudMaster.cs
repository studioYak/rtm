using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UI;

public class HudMaster : MonoBehaviour {

    public enum HudType { Life, Special };

    public GameObject hudLife;
    public GameObject hudSpecial;
	public GameObject hudXPBar;
	public GameObject hudXPText;



    // Use this for initialization
    void Start () {
       // hudLife = GameObject.Find("hud_life");
       // hudSpecial = GameObject.Find("hud_special");
		Color32 newColor;
		if (GameModel.HerosInGame [0].GetType ().ToString () == "Warrior") {
			newColor = new Color32(43, 170, 11, 184);
		} else if (GameModel.HerosInGame [0].GetType ().ToString () == "Wizard") {
			newColor = new Color32 (25, 1, 202, 184);
		} else {
			newColor = new Color32 (20, 20, 20, 184);
		}

		hudSpecial.GetComponent<Image> ().color = (Color) newColor;

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

	/**
	 * @param HudType _hudType Is it power HUD or Life HUD
	 * @param float _levelPercent Number between 0 and 100 that will graphically sets the filling percentage of the gauje
	 **/
    public void setLevel(HudType _hudType, float _levelPercent)
    {
        GameObject hudTarget = null;

        if (_hudType == HudType.Life)
        {
            hudTarget = hudLife;
        }
        else if (_hudType == HudType.Special)
        {
            hudTarget = hudSpecial;
        }

        if (hudTarget != null)
        {
            hudTarget.transform.localScale = new Vector3(1, _levelPercent/100, 1);
        }
    }

	public void updateXP(float xpPercent, int level) {

		hudXPBar.transform.localScale = new Vector3 (xpPercent / 100.0f, 1.0f, 1.0f);

		//GameObject levelDigit = hudXP.transform.FindChild ("levelText").gameObject;
		hudXPText.GetComponentInChildren<Text>().text = level.ToString();


	}


}
