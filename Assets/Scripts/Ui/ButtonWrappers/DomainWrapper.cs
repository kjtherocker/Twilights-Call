﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class DomainWrapper : MonoBehaviour
{


    public Creatures m_ButtonTurnHolder;

    private List<Creatures> m_ListReference;
    public List<Material> m_ElementIconsList;
    public List<Material> m_CardDesigns;

    public Skills.ElementalType m_ElementalIconType;
    public Skills.SkillType m_SkillType;

    public Skills m_ButtonSkill;
    public Domain m_Domain;
    public Button m_Button;
    public TextMeshProUGUI m_CostToUseText;
    public TextMeshProUGUI m_Text_NameOfSkill;
    public Image m_Image_CardDesign;
    public Image m_Image_ElementalIcon;

    public int CardPower;
    
    public string m_NameOfSkill;

    public TextMeshProUGUI m_CardRange;

    public Image m_LeftArrow;
    public Image m_RightArrow;
    

    Color m_Color_TransparentWhite;
    Color m_Color_White;

    // Use this for initialization
    void Start()
    {
         m_Color_TransparentWhite = new Color(1, 1, 1, 0.5f);
         m_Color_White = new Color(1, 1, 1, 1);
         CardPower = 1;
    }
    
    public void SetElementalIcon(Skills.ElementalType aSkills, string sourceName = "Global")
    {
        m_Image_ElementalIcon.material = m_ElementIconsList[(int)aSkills];
    }

    public void SetCardDesign(Skills.SkillType aSkills, string sourceName = "Global")
    {
        m_Image_CardDesign.material = m_CardDesigns[(int)aSkills];
    }



    public void SetupButton(Creatures a_TurnHolder, Skills a_Skill)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_ButtonSkill = a_Skill;
        m_SkillType = a_Skill.GetSkillType();
        SetCardDesign(m_SkillType);

        SetElementalIcon(a_Skill.GetElementalType());
        m_Text_NameOfSkill.text = a_Skill.SkillName;
        m_CostToUseText.text = a_Skill.m_CostToUse.ToString();
        
        if (m_ButtonTurnHolder.CurrentMana <= m_ButtonSkill.GetCostToUse())
        {
            m_Text_NameOfSkill.color = m_Color_TransparentWhite;
        }
        else if (m_ButtonTurnHolder.CurrentMana >= m_ButtonSkill.GetCostToUse())
        {
            m_Text_NameOfSkill.color = m_Color_White;
        }
    }
    
    public void SetupDomain(Creatures a_TurnHolder, Domain aDomain)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_Domain = aDomain;
        SetCardDesign(Skills.SkillType.Domain);
        SetElementalIcon(aDomain.m_ElementalType);
        m_Text_NameOfSkill.text = aDomain.DomainName;
        m_CostToUseText.text = aDomain.m_CostToUse.ToString();
    }

    public void SetAsNotInteractable()
    {
        m_Button.interactable = false;
    }

    public void CardPowerSet(int aPower)
    {
        if (aPower <= 0 || aPower == 4)
        {
            return;
        }

        Debug.Log("Increase Card Power");

        GameManager.Instance.m_BattleCamera.m_CombatInputLayer.SetDomainPhase(aPower);
        m_CardRange.text = "Lv. " + aPower;
    }






    public void ToDestroy()
    {
        Destroy(gameObject);
    }
}
