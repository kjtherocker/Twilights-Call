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
    
    public bool m_SwapBetweenSkillDomain;

    public GameObject EmptySkillCard;
    public GameObject EmptyDevourCard;
    public GameObject EmptyDomainCard;
    
    DomainBoardState m_DomainBoardState;
    
    public Vector3 m_CenterCardPosition;
    // Use this for initialization
    public override void Initialize()
    {
        m_SkillBoardPointerPosition = 0;
        m_CenterCardPosition = new Vector3(-38, -211, 0);
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();
        //m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }

    
    public override void ResetCursorPosition()
    {
        m_CursorYMax = 0;
        m_CursorYCurrent = 0;
        m_CursorYMin = 0;
        
        m_CursorXMax = m_CurrentSkillMenuButtonsMenu.Count - 1;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;

    }

    public void SetSkill()
    {
        if (m_DomainBoardState != DomainBoardState.Selecting)
        {
            return;
        }

        m_DomainBoardState = DomainBoardState.Selected;


        if (m_SkillBoardPointerPosition == 0)
        {
            GameManager.Instance.BattleCamera.m_CombatInputLayer.SetDomainPhase(m_SkillBoardCreature.m_Domain);
            m_CurrentSkillMenuButtonsMenu[1].gameObject.SetActive(false);
        }
        else if (m_SkillBoardPointerPosition == 1)
        {
            GameManager.Instance.BattleCamera.m_CombatInputLayer.SetDevourPhase(); 
            m_CurrentSkillMenuButtonsMenu[0].gameObject.SetActive(false);
        }


        SetSkillInstantly(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);
        //StartCoroutine(MoveSkillToSide( m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]));

        
        
        InputManager.Instance.m_MovementControls.Enable();
        //GameManager.Instance.UiManager.PopScreen();
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



    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        int OriginalPointerPosition = m_SkillBoardPointerPosition;
        
        if (aCursorX > 0)
        {
            m_SkillBoardPointerPosition++;
        }
        
        if (aCursorX < 0)
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

        
        
        AnimatedCardMovementDown(m_CurrentSkillMenuButtonsMenu[OriginalPointerPosition]);
        AnimatedCardMovementToCenter(m_CurrentSkillMenuButtonsMenu[m_SkillBoardPointerPosition]);
        
    }
}
