﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class DomainWrapper : MonoBehaviour
{


    public enum DomainWrapperType
    {
        Domain,
        Devour
    }

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

    public DomainWrapperType m_DomainWrapperType;
    
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



    public void SetupButton(Creatures a_TurnHolder, Domain aDomain,DomainWrapperType aWrapperType)
    {
        m_ButtonTurnHolder = a_TurnHolder;
        m_ButtonSkill = aDomain;
        m_SkillType = aDomain.GetSkillType();
        SetCardDesign(m_SkillType);

        m_DomainWrapperType = aWrapperType;
        
        m_ButtonTurnHolder = a_TurnHolder;
        m_Domain = aDomain;
        SetCardDesign(Skills.SkillType.Domain);
        SetElementalIcon(aDomain.m_ElementalType);
    }
    
    public void SetupDomain(Creatures a_TurnHolder, Domain aDomain)
    {
       
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
        
        
        if (aPower > 1)
        {
            m_LeftArrow.gameObject.SetActive(true);
        }
        else if(aPower == 1)
        {
            m_LeftArrow.gameObject.SetActive(false);
        }

        if (m_ButtonTurnHolder.CurrentDomainpoints > aPower )
        {
            m_RightArrow.gameObject.SetActive(true);
        }
        else
        {
            m_RightArrow.gameObject.SetActive(false);
        }


        if (m_DomainWrapperType == DomainWrapperType.Domain)
        {
            GameManager.Instance.m_TacticsCameraController.m_CombatInputLayer.SetDomainPhase(aPower);
        }
        else if (m_DomainWrapperType == DomainWrapperType.Devour)
        {
            GameManager.Instance.m_TacticsCameraController.m_CombatInputLayer.SetDevourPhase(aPower);
        }

        m_CardRange.text = "Lv. " + aPower;
    }






    public void ToDestroy()
    {
        Destroy(gameObject);
    }
}
