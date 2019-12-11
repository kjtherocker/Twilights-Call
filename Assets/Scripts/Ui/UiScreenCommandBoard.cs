using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiScreenCommandBoard : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Creatures m_CommandboardCreature;
    public TextMeshProUGUI m_MovementText;
    public TextMeshProUGUI m_Attack;
    public TextMeshProUGUI m_Skill;
    public int m_CommandBoardPointerPosition;


    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.XButton.performed += XButton => PlayerMovement();
        m_MenuControls.Player.SquareButton.performed += SquareButton => SpawnSkillBoard();
        m_MenuControls.Disable();
    }

    public override void OnPop()
    {
        //m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();
    }

    public override void OnPush()
    {
        gameObject.SetActive(true);
        GameManager.Instance.m_InputManager.m_MovementControls.Disable();
        m_MenuControls.Enable();
       // m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossIn");
    }

    public void TurnCommandBoardOff()
    {
       m_MenuControls.Disable();
       gameObject.SetActive(false);
       m_CommandBoardPointerPosition = 0;


    }

    public void SetCreatureReference(Creatures aCreature)
    {
        m_CommandboardCreature = aCreature;

    }

    public void PlayerMovement()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasMovedForThisTurn)
        {
            return;
        }
        GameManager.Instance.m_InputManager.m_MovementControls.Enable();
        m_CommandboardCreature.m_CreatureAi.FindAllPaths();
        GameManager.Instance.BattleCamera.m_MovementHasBeenCalculated = true;
        GameManager.Instance.UiManager.PopScreen();
        
    }


    public void SpawnSkillBoard()
    {
        if (m_CommandboardCreature.m_CreatureAi.m_HasAttackedForThisTurn)
        {
            return;
        }

        m_MenuControls.Disable();
        
        GameManager.Instance.UiManager.PopScreen();
        GameManager.Instance.UiManager.PushScreen(UiManager.Screen.SkillBoard);

        UiSkillBoard ScreenTemp =
            GameManager.Instance.UiManager.GetScreen(UiManager.Screen.SkillBoard) as UiSkillBoard;

        ScreenTemp.SpawnSkills(m_CommandboardCreature);
       
        
    }


}
