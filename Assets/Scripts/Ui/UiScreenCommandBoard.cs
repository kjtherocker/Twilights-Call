﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Creatures m_CommandboardCreature;
    public Button m_MovementButton;
    public TextMeshProUGUI m_MovementText;
    public TextMeshProUGUI m_Attack;
    public TextMeshProUGUI m_Skill;
    public int m_CommandBoardPointerPosition;
    
    // Use this for initialization
	void Start ()
    {
        m_CommandBoardPointerPosition = 0;
        GameManager.Instance.m_InputManager.m_MenuControls.Player.XButton.performed += XButton => PlayerMovement();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_CommandBoardPointerPosition < 0)
        {
            m_CommandBoardPointerPosition = 0;
        }
        else if (m_CommandBoardPointerPosition > 2)
        {
            m_CommandBoardPointerPosition = 2;
        }
    }

    public override void OnPop()
    {
        //m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();
        

    }

    public override void OnPush()
    {
        gameObject.SetActive(true);
        GameManager.Instance.m_InputManager.m_MenuControls.Enable();
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossIn");
    }

    public void TurnCommandBoardOff()
    {
        GameManager.Instance.m_InputManager.m_MenuControls.Disable();
        gameObject.SetActive(false);
        m_CommandBoardPointerPosition = 0;


    }

    public void SetCreatureReference(Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;

        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn == true)
        {
            m_MovementButton.interactable = false;
        }
        else
        {
            m_MovementButton.interactable = true;
        }

    }

    public void PlayerMovement()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn == false)
        {
            m_CommandboardCreature.m_CreatureAi.FindAllPaths();
            GameManager.Instance.BattleCamera.m_MovementHasBeenCalculated = true;
            GameManager.Instance.UiManager.PopScreen();
        }
    }


    public void SpawnSkillBoard()
    {
        GameManager.Instance.UiManager.PushScreen(UiManager.Screen.SkillBoard);

        UiSkillBoard ScreenTemp =
            GameManager.Instance.UiManager.GetScreen(UiManager.Screen.SkillBoard) as UiSkillBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
    }

    public void MoveCommandBoardPositionUp(int a_PointerPosition)
    {
        m_CommandBoardPointerPosition = a_PointerPosition;

        if (m_CommandBoardPointerPosition == 0)
        {
            PlayerMovement();
        }
        if (m_CommandBoardPointerPosition == 1)
        {

            GameManager.Instance.UiManager.PopScreen();
            GameManager.Instance.BattleCamera.SetAttackPhase(m_CommandboardCreature.m_Attack);
        }
        if (m_CommandBoardPointerPosition == 2)
        {
            SpawnSkillBoard();
        }
        if (m_CommandBoardPointerPosition == 3)
        {

        }
    }


}