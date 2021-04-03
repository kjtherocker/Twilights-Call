using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Creatures m_CommandboardCreature;

    public GameObject m_CommandObjects;
    
    public TextMeshProUGUI m_MovementText;
    public TextMeshProUGUI m_Attack;
    public TextMeshProUGUI m_Skill;
    public int m_CommandBoardPointerPosition;


    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.XButton.performed += XButton => PlayerMovement();
        m_MenuControls.Player.SquareButton.performed += SquareButton => SpawnSkillBoard();
        m_MenuControls.Player.TriangleButton.performed += TriangleButton => SpawnDomainBoard();
        m_MenuControls.Player.CircleButton.performed += CircleButton => ReturnToLastScreen();

        m_MenuControls.Disable();
    }

    public override void OnPop()
    {
        m_MenuControls.Disable();
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();
    }

    public override void OnPush()
    {
        gameObject.SetActive(true);
        InputManager.Instance.m_MovementControls.Disable();
        m_MenuControls.Enable();
        

        
        m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossIn");
    }
    public void TurnCommandBoardOff()
    {
       m_MenuControls.Disable();
       gameObject.SetActive(false);
       m_CommandBoardPointerPosition = 0;


    }

    public void Update()
    {
        if (m_CommandboardCreature != null)
        {
            Vector2 screenPosition = GameManager.Instance.MTacticsCameraController.GetComponent<Camera>()
                .WorldToScreenPoint(m_CommandboardCreature.m_CreatureAi.transform.position + Vector3.up);
            m_CommandObjects.transform.position = screenPosition;
        }
    }

    public void SetCreatureReference(Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;
        Vector2 screenPosition = GameManager.Instance.MTacticsCameraController.GetComponent<Camera>().WorldToScreenPoint(m_CommandboardCreature.m_CreatureAi.transform.position + Vector3.up);
        m_CommandObjects.transform.position = screenPosition;
    }

    public void PlayerMovement()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn)
        {
            return;
        }
        InputManager.Instance.m_MovementControls.Enable();
        GameManager.Instance.MTacticsCameraController.m_CombatInputLayer.m_CombatInputState =
            CombatInputLayer.CombatInputState.Walk;
        
     //   m_CommandboardCreature.m_CreatureAi.FindAllPaths();
        
        UiManager.Instance.PopScreen();
        
    }

    
    public void SpawnDomainBoard()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasAttackedForThisTurn)
        {
            return;
        }

        m_MenuControls.Disable();

        m_CommandboardCreature.m_CreatureAi.DeselectAllPaths();
        
        UiManager.Instance.PopScreen();
        UiManager.Instance.PushScreen(UiManager.Screen.DomainBoard);

        UiDomainBoard ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.Screen.DomainBoard) as UiDomainBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }

    public void SpawnSkillBoard()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasAttackedForThisTurn)
        {
            return;
        }

        m_MenuControls.Disable();
        
        m_CommandboardCreature.m_CreatureAi.DeselectAllPaths();
        
        UiManager.Instance.PopScreen();
        UiManager.Instance.PushScreen(UiManager.Screen.SkillBoard);

        UiSkillBoard ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.Screen.SkillBoard) as UiSkillBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }


    public override void ReturnToLastScreen()
    {
        base.ReturnToLastScreen();

        GameManager.Instance.MTacticsCameraController.SetCameraState(TacticsCameraController.CameraState.Normal);
        InputManager.Instance.m_MovementControls.Enable();
    }
}
