using UnityEngine;
using System.Collections;

public class HudMaster : MonoBehaviour {

    public enum HudType { Life, Special };

    public GameObject hudLife;
    public GameObject hudSpecial;


    // Use this for initialization
    void Start () {
       // hudLife = GameObject.Find("hud_life");
       // hudSpecial = GameObject.Find("hud_special");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

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
}
