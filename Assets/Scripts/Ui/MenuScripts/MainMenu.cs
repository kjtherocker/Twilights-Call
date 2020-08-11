using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : UiScreen
{
    public Animator m_CommandBoardAnimator;
    public Creatures m_CommandboardCreature;

    public GameObject m_CommandObjects;
    
    public List<TextMeshProUGUI> m_MovementText;
    public int m_CommandBoardPointerPosition;
    public Animator m_HeartAnimator;
    public GameObject m_Cutscene;

    public delegate void Method();

    private List<Method> m_MenuItems;
    
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();
       
        m_MenuControls.Player.XButton.performed += XButton => SelectMenuItem();
        m_MenuControls.Disable();

        m_MenuItems.Add(Playgame);
       // m_MenuItems.Add(Playgame);
        m_MenuItems.Add(ExitGame);
    }

    public override void MenuSelection(int aCursorX, int aCursorY)
    {
      // m_ArenaTabs[m_CursorYCurrent].ChangeColorToSelected();        
      // m_ArenaTabs[m_CursorYPrevious].ChangeColorToDefault();

    }

    public void SelectMenuItem()
    {
        m_MenuItems[m_CursorYCurrent](); 
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
        if(m_HeartAnimator.GetCurrentAnimatorStateInfo(0).IsName("PatchworkHeartFail") && 
           m_HeartAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene(1);
        }
    }



    public void Playgame()
    {
        m_HeartAnimator.SetBool("PlayButton", true);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
} 
