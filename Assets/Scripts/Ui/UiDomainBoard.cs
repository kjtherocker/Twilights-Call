using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiDomainBoard : UiScreen
{

    enum DomainBoardState
    {
        Selecting,
        Selected,
    }

    public Creatures m_SkillBoardCreature;
    public DomainWrapper m_ButtonReference;
    public List<DomainWrapper> m_CurrentSkillMenuButtonsMenu;

    public Animator m_Animator_SkillGroup;
    public DomainWrapper m_DomainButton;

    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;

    public int m_CardPower; 
    
    public bool m_SwapBetweenSkillDomain;

    public DomainWrapper m_SelectedDomainWrapper;
    
    public GameObject EmptySkillCard;
    public GameObject EmptyDevourCard;
    public GameObject EmptyDomainCard;
    
    private DomainBoardState m_DomainBoardState;
    
    public Vector3 m_CenterCardPosition;
    // Use this for initialization
    public override void Initialize()
    {
        m_SkillBoardPointerPosition = 0;
        m_CenterCardPosition = new Vector3(-38, -211, 0);
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => ActivateSelectedCard();
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();



        ResetCursorPosition();
        m_DomainBoardState = DomainBoardState.Selecting;
        //m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }

    
    public override void OnPop()
    {
        gameObject.SetActive((false));
        m_MenuControls.Disable();
 

        m_CurrentSkillMenuButtonsMenu[0].gameObject.transform.position = EmptyDomainCard.transform.position;
        m_CurrentSkillMenuButtonsMenu[1].gameObject.transform.position = EmptyDevourCard.transform.position;
        
        //   m_Animator_SkillGroup.gameObject.SetActive(false);
        // m_Animator_SkillGroup.SetBool("ZoomIn",false);
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
        InputManager.Instance.m_MovementControls.Disable();
        ResetCursorPosition();
        // m_Animator_SkillGroup.SetBool("ZoomIn",true);
        //  m_Animator_SkillGroup.gameObject.SetActive(true);

        foreach (var VARIABLE in m_CurrentSkillMenuButtonsMenu)
        {
            VARIABLE.gameObject.SetActive(true);
        }
     
        m_MenuControls.Enable();
        m_DomainBoardState = DomainBoardState.Selecting;
    }
    
    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;


        if (m_DomainBoardState == DomainBoardState.Selecting)
        {
            m_CursorXCurrent = MenuDirectionCalculationLooping(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        }
        else if (m_DomainBoardState == DomainBoardState.Selected)
        {
            m_CursorXCurrent = MenuDirectionCalculationEndInvertAxis(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        }

        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void ResetCursorPosition()
    {
        m_CursorXMax = 1;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;

    }

    public void SelectedCardEmpowerValues()
    {
        m_CursorXMax = 4;
        m_CursorXCurrent = 1;
        m_CursorXMin = 1;
    }

    public void ActivateSelectedCard()
    {
        if (m_DomainBoardState != DomainBoardState.Selected)
        {
            return;
        }

        Debug.Log("Activated SelectedCard");
        
        GameManager.Instance.BattleCamera.m_CombatInputLayer.ActivatedDomain();

    }
    public void SetSkill()
    {
        if (m_DomainBoardState != DomainBoardState.Selecting)
        {
            return;
        }
        


        if (m_SkillBoardPointerPosition == 0)
        {
            GameManager.Instance.BattleCamera.m_CombatInputLayer.SetDomainPhase(1);
            m_SelectedDomainWrapper = m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition];
            m_CurrentSkillMenuButtonsMenu[1].gameObject.SetActive(false);
        }
        else if (m_SkillBoardPointerPosition == 1)
        {
            GameManager.Instance.BattleCamera.m_CombatInputLayer.SetDevourPhase(1);
            m_SelectedDomainWrapper = m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition];
            m_CurrentSkillMenuButtonsMenu[0].gameObject.SetActive(false);
        }

        SetSkillInstantly(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);

        SelectedCardEmpowerValues();
        m_DomainBoardState = DomainBoardState.Selected;
    }

    public void SpawnSkills(Creatures aCreatures)
    {

        m_SkillBoardCreature = aCreatures;
        
        m_CurrentSkillMenuButtonsMenu[0].SetupButton(m_SkillBoardCreature,m_SkillBoardCreature.m_Domain);
        m_CurrentSkillMenuButtonsMenu[1].SetupButton(m_SkillBoardCreature,new Devour());


        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[0]);
    }



    public void SetSkillInstantly(DomainWrapper aButtonSkillWrapper)
    {
        aButtonSkillWrapper.gameObject.transform.position = EmptySkillCard.gameObject.transform.position;
    }


    public IEnumerator MoveSkillToSide(DomainWrapper aButtonSkillWrapper)
    {

        if (aButtonSkillWrapper.gameObject.transform.position == EmptySkillCard.gameObject.transform.position)
        {
            yield break;
        }

        aButtonSkillWrapper.gameObject.transform.position = Vector3.MoveTowards( aButtonSkillWrapper.gameObject.transform.position,
            EmptySkillCard.gameObject.transform.position, Time.deltaTime * 8);
        
        yield return new WaitForEndOfFrame();
    }
    
    public void SelectingCard()
    {
        AnimatedCardMovementDown(m_CurrentSkillMenuButtonsMenu[m_CursorXPrevious]);
        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[m_CursorXCurrent]);
    }

    public void EmpowerCard()
    {
     
        if (m_DomainBoardState != DomainBoardState.Selected)
        {
            return;
        }

        if (m_CursorXCurrent == m_CursorXPrevious)
        {
            return;
        }

        
        m_SelectedDomainWrapper.CardPowerSet(m_CursorXCurrent);
        

    }

    public override void MenuSelection(int aCursorX, int aCursorY)
    {

        if (m_DomainBoardState == DomainBoardState.Selecting)
        {
            SelectingCard();
        }
        else if(m_DomainBoardState == DomainBoardState.Selected)
        {
            EmpowerCard();
        }
    }
    
    
    
    public void AnimatedCardMovementToCenter(DomainWrapper a_SkillWrapper)
    {
        a_SkillWrapper.transform.position = new Vector3(a_SkillWrapper.transform.position.x, 150, a_SkillWrapper.transform.position.z);

        //      m_DescriptionText.text = a_SkillWrapper.m_ButtonSkill.SkillDescription;
    }
    
    public void AnimatedCardMovementDown(DomainWrapper a_SkillWrapper)
    {
        a_SkillWrapper.transform.position = new Vector3(a_SkillWrapper.transform.position.x, 125, a_SkillWrapper.transform.position.z);
     
        //   m_DescriptionText.text = a_SkillWrapper.m_ButtonSkill.SkillDescription;
    }

}
