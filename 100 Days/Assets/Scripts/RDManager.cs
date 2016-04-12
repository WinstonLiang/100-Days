using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RDManager : MonoBehaviour 
{
    public List<SkillTree> skillTrees;
    public Image locked;

    private List<UnitClass> allPlayerUnits;    

    void Start()
    {
        allPlayerUnits = GetComponent<UnitManager>().allPlayerUnits;
        initSkillTrees();
        checkMaxLevels();
    }

    void initSkillTrees()
    {
        //Order is important
        SkillTree assaultSkill = new SkillTree();
        SkillTree defenderSkill = new SkillTree();
        SkillTree medicSkill = new SkillTree();

        skillTrees = new List<SkillTree>();
        skillTrees.Add(assaultSkill);
        skillTrees.Add(defenderSkill);
        skillTrees.Add(medicSkill);
    }

    //Iterate through allPlayerUnits and set the highest levels
    public void checkMaxLevels()
    {
        foreach (UnitClass unit in allPlayerUnits)
        {
            if (unit.classType == 1) //Assault
                skillTrees[0].UpdateMaxLvl(unit.level);
            else if (unit.classType == 2) //Defender
                skillTrees[1].UpdateMaxLvl(unit.level);
            else //Medic
                skillTrees[2].UpdateMaxLvl(unit.level);
        }
    }

    //Set the button images and text for BOTH locked and unlocked skills
    public void SetLockedSkill(bool locked, Transform panel)
    {
        if (locked)
        {
            //Turn lock image on
            panel.GetChild(panel.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            //Hide lock image
            panel.GetChild(panel.childCount - 1).gameObject.SetActive(false);
        }
    }
}

public class SkillTree
{
    public int currentMaxLevel;
    public bool ability1Lock, ability2Lock, ability3Lock;
    private int ability1Unlock = 2, 
                ability2Unlock = 10, 
                ability3Unlock = 20;
    
    public SkillTree()
    {
        ability1Lock = ability2Lock = ability3Lock = true;
    }

    //Use bool to indicate whether an ability was unlocked or not
    public bool UpdateMaxLvl(int level)
    {
        if (level > currentMaxLevel)
        {
            currentMaxLevel = level;
            UnlockAbility(level);
            return (level == ability1Unlock ||
                level == ability2Unlock ||
                level == ability3Unlock);                
        }
        return false;
    }

    //Unlock abilities if level meets req. level and is locked
    void UnlockAbility(int level)
    {
        if (ability1Lock && level >= ability1Unlock)
        {
            ability1Lock = false;
        }

        if (ability2Lock && level >= ability2Unlock)
        {
            ability2Lock = false;
        }

        if (ability3Lock && level >= ability3Unlock)
        {
            ability3Lock = false;
        }
    }
}
