using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSkillBoard : UiScreen
{

    public Creatures m_SkillBoardCreature;
    public ButtonSkillWrapper m_ButtonReference;
    public List<ButtonSkillWrapper> m_CurrentSkillMenuButtonsMenu;
    public Animator m_Animator_SkillGroup;
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
       
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();
        //m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }
	// Update is called once per frame
    
    public override void ResetCursorPosition()
    {
        m_CursorXMax = m_CurrentSkillMenuButtonsMenu.Count -1;
        m_CursorXCurrent = 0;

    }
    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;

        
        m_CursorXCurrent = MenuDirectionCalculationLooping(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_SkillBoardPointerPosition = aCursorX;

        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[m_CursorXPrevious]);
        AnimatedCardMovementDown(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);

    }

    public void SetSkill()
    {

        GameManager.Instance.BattleCamera.m_CombatInputLayer.SetAttackPhase(m_SkillBoardCreature.m_Skills[m_SkillBoardPointerPosition]);
 
        InputManager.Instance.m_MovementControls.Enable();
        GameManager.Instance.UiManager.PopScreen();
    }

    public override void OnPop()
    {
        gameObject.SetActive((false));
        m_MenuControls.Disable();
     //   m_Animator_SkillGroup.gameObject.SetActive(false);
       // m_Animator_SkillGroup.SetBool("ZoomIn",false);
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
        InputManager.Instance.m_MovementControls.Disable();
       // m_Animator_SkillGroup.SetBool("ZoomIn",true);
     //  m_Animator_SkillGroup.gameObject.SetActive(true);
        m_MenuControls.Enable();
    }

    public void SpawnSkills(Creatures aCreatures)
    {

        m_SkillBoardCreature = aCreatures;

        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu[i].gameObject.SetActive(false);

        }
        
        for (int i = 0; i < m_SkillBoardCreature.m_Skills.Count; i++)
        {
            m_CurrentSkillMenuButtonsMenu[i].gameObject.SetActive(true);
            m_CurrentSkillMenuButtonsMenu[i].SetupButton(m_SkillBoardCreature, m_SkillBoardCreature.m_Skills[i]);
        }
        
       ResetCursorPosition();
        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[0]);
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
}
