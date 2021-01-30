﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMemoria : UiScreen
{

    public Creatures m_MemoriaCreature;
    public List<ButtonSkillWrapper> m_MemoriaSkills;
    public List<ButtonSkillWrapper> m_CreatureSkills;
    public Animator m_Animator_SkillGroup;

    public TextMeshProUGUI m_DescriptionText;
    public int m_SkillBoardPointerPosition;

    private Memoria m_Memoria;

    public float m_CardMovementSpeed;
    
    public bool m_SwapBetweenSkillDomain;
    
    private Vector3 m_CenterCardPosition;

    public List<Vector3> m_DefaultMemoriaCardPositions;
    
    public GameObject m_MemoriaScream;
    public GameObject m_Board;

    private Animator m_ScreamAnimator;
    private Animator m_BoardAnimator;
    
    // Use this for initialization
    public override void Initialize()
    {
        m_SkillBoardPointerPosition = 0;
        m_CenterCardPosition = new Vector3(-38, -211, 0);
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SetSkill();

        m_CardMovementSpeed = 8;


        m_ScreamAnimator = m_MemoriaScream.GetComponent<Animator>();
        m_BoardAnimator = m_Board.GetComponent<Animator>();

        foreach (ButtonSkillWrapper aMemoriaSkill in m_MemoriaSkills)
        {
            m_DefaultMemoriaCardPositions.Add(aMemoriaSkill.transform.localPosition);    
        }
        
        //m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }
    public override void ResetCursorPosition()
    {
        m_CursorXMax = m_MemoriaSkills.Count -1;
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
        
        SetCardHighlight(m_MemoriaSkills[m_SkillBoardPointerPosition].gameObject);
        
        m_DescriptionText.text = m_MemoriaSkills[m_SkillBoardPointerPosition].m_ButtonSkill.SkillDescription;
    }

    IEnumerator MoveCardIntoDeck(Transform aObject, Transform aTarget, float aTimeUntilDone )
    {
        Vector3 NewNodePosition = new Vector3(aTarget.transform.position.x,aTarget.transform.position.y ,
            aTarget.transform.position.z);
        
        float timeTaken = 0.0f;
        
        while (aTimeUntilDone - timeTaken > 0)
        {
            if (Vector3.Distance(aObject.transform.position, NewNodePosition) < 0.9f)
            {
                timeTaken = aTimeUntilDone;
            }

            timeTaken += Time.deltaTime;
            aObject.position = Vector3.Lerp(aObject.position, NewNodePosition, timeTaken /aTimeUntilDone );
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        
        m_MemoriaCreature.m_Skills.Add(m_MemoriaSkills[m_SkillBoardPointerPosition].m_ButtonSkill);
        InputManager.Instance.m_MovementControls.Enable();
        OnPop();
        m_Memoria.DestroyMemoria();
        
    }

    public void SetSkill()
    {
        
        m_MenuControls.Disable();
        StartCoroutine(MoveCardIntoDeck(m_MemoriaSkills[m_SkillBoardPointerPosition].transform,
            m_CreatureSkills[m_MemoriaCreature.m_Skills.Count].transform, m_CardMovementSpeed ));
        

    }

    public override void OnPop()
    {
        gameObject.SetActive((false));
        m_MenuControls.Disable();

        for (int i = 0; i < m_MemoriaSkills.Count; i++)
        {
            m_MemoriaSkills[i].transform.localPosition = m_DefaultMemoriaCardPositions[i];
        }
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));
        InputManager.Instance.m_MovementControls.Disable();

        m_MenuControls.Enable();
        
      //  m_ScreamAnimator.SetTrigger("t_Push"); 
        m_BoardAnimator.SetTrigger("t_Push");
    }

    public void SetMemoriaScreen(Creatures aCreature, Memoria aMemoria)
    {
        m_MemoriaCreature = aCreature;

        m_Memoria = aMemoria;
        
        for (int i = 0; i < m_MemoriaSkills.Count; i++)
        {
            m_MemoriaSkills[i].gameObject.SetActive(false);

        }
        
        for (int i = 0; i < aMemoria.m_Skills.Count; i++)
        {
            m_MemoriaSkills[i].gameObject.SetActive(true);

            m_MemoriaSkills[i].SetupButton(aCreature, aMemoria.m_Skills[i]);
        }
        
        
        for (int i = 0; i < m_CreatureSkills.Count; i++)
        {
            m_CreatureSkills[i].gameObject.SetActive(false);

        }
        
        for (int i = 0; i < m_MemoriaCreature.m_Skills.Count; i++)
        {
            m_CreatureSkills[i].gameObject.SetActive(true);
            m_CreatureSkills[i].SetupButton(aCreature, aCreature.m_Skills[i]);
        }

        
        ResetCursorPosition();
        
        SetCardHighlight(m_MemoriaSkills[0].gameObject);

        m_DescriptionText.text = m_MemoriaSkills[0].m_ButtonSkill.SkillDescription;
    }
    
}
