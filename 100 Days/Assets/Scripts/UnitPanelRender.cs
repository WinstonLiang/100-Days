using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitPanelRender : MonoBehaviour 
{
    public Text levelTxt, classTxt, nameTxt, hpTxt,
                  atkTxt, defTxt, speedTxt, critTxt, dodgeTxt, squadTxt;
    public Image classImg;

    public void renderUnitData(UnitClass unit)
    {
        levelTxt.text = unit.level.ToString();
        classTxt.text = unit.classToString();
        nameTxt.text = unit.firstName + " " + unit.lastName;
        hpTxt.text = unit.currentHealth.ToString() + " / " + unit.maxHealth.ToString();
        atkTxt.text = unit.att.ToString();
        defTxt.text = unit.def.ToString();
        speedTxt.text = unit.maxSpeed.ToString();
        critTxt.text = unit.crit.ToString();
        dodgeTxt.text = unit.dodge.ToString();
        squadTxt.text = unit.squad == 0 ? "Active" : "Reserve";
    }
}
