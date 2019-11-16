﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSkillBoard : UiScreen
{

    public Creatures m_SkillBoardCreature;
    public ButtonSkillWrapper m_ButtonReference;
    public List<ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;
    
    public ButtonSkillWrapper m_DomainButton;

    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;
    
    public bool m_SwapBetweenSkillDomain;
    
    public Vector3 m_CenterCardPosition;
    // Use this for initialization
    public override void Initialize()
    {
        m_SkillBoardPointerPosition = 0;
        m_CenterCardPosition = new Vector3(-38, -211, 0);
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.Movement.performed += movement => MoveCommandBoardPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();
        m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }
	// Update is called once per frame
	void Update ()
    {
    }

    public void SetSkill()
    {
        if(m_SwapBetweenSkillDomain == false)
        {
            GameManager.Instance.BattleCamera.SetAttackPhase(m_SkillBoardCreature.m_Skills[m_SkillBoardPointerPosition]);
        }
        else
        {
            GameManager.Instance.BattleCamera.SetDomainPhase(m_SkillBoardCreature.m_Domain);
        }
        GameManager.Instance.m_InputManager.m_MovementControls.Enable();
        GameManager.Instance.UiManager.PopScreen();
    }

    public override void OnPop()
    {
        DeleteSkills();
        gameObject.SetActive((false));
        m_MenuControls.Disable();
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
        GameManager.Instance.m_InputManager.m_MovementControls.Disable();
        m_MenuControls.Enable();
    }

    public void SpawnSkills(Creatures aCreatures)
    {

        m_SkillBoardCreature = aCreatures;

        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu.Add(Instantiate<ButtonSkillWrapper>(m_ButtonReference, gameObject.transform));
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i], this);
            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.SetParent(this.transform, false);

            m_CurrentSkillMenuButtonsMenu[i].gameObject.transform.position = new Vector3(200 + i * 150, 125, 0);

        }
        
        m_DomainButton = Instantiate<ButtonSkillWrapper>(m_ButtonReference, gameObject.transform);
        m_DomainButton.SetupDomain(m_SkillBoardCreature, m_SkillBoardCreature.m_Domain, this);
        m_DomainButton.gameObject.transform.position = new Vector3(850 , 300, 0);

        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[0]);
    }

    public void DeleteSkills()
    {
        for (int i = m_SkillBoardCreature.m_Skills.Count - 1; i > -1; i--)
        {
            Destroy(m_CurrentSkillMenuButtonsMenu[i].gameObject);
            m_CurrentSkillMenuButtonsMenu.RemoveAt(i);
        }
        
        Destroy(m_DomainButton.gameObject);
    }

    public void AnimatedCardMovementToCenter(ButtonSkillWrapper a_SkillWrapper)
    {
        a_SkillWrapper.transform.position = new Vector3(a_SkillWrapper.transform.position.x, 150, a_SkillWrapper.transform.position.z);

        m_DescriptionText.text = a_SkillWrapper.m_ButtonSkill.SkillDescription;
    }
    
    public void AnimatedCardMovementDown(ButtonSkillWrapper a_SkillWrapper)
    {
        a_SkillWrapper.transform.position = new Vector3(a_SkillWrapper.transform.position.x, 125, a_SkillWrapper.transform.position.z);

        m_DescriptionText.text = a_SkillWrapper.m_ButtonSkill.SkillDescription;
    }

    public void MoveCommandBoardPosition(Vector2 aMovement)
    {
        int OriginalPointerPosition = m_SkillBoardPointerPosition;
        
        if (aMovement.x > 0)
        {
            m_SkillBoardPointerPosition++;
        }
        
        if (aMovement.x < 0)
        {
            m_SkillBoardPointerPosition--;
        }

        if (m_SkillBoardPointerPosition < 0)
        {
            m_SkillBoardPointerPosition = m_CurrentSkillMenuButtonsMenu.Count - 1;
        }
        else if (m_SkillBoardPointerPosition > m_CurrentSkillMenuButtonsMenu.Count -1)
        {
            m_SkillBoardPointerPosition = 0;
        }
        //if(aMovement)
        
        
        AnimatedCardMovementDown(m_CurrentSkillMenuButtonsMenu[OriginalPointerPosition]);

        if (aMovement.y > 0 )
        {
            m_SwapBetweenSkillDomain = true;
        }
        else if(aMovement.y < 0 )
        {
            m_SwapBetweenSkillDomain = false;
        }
        else
        {
            AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);
        }
    }
}
