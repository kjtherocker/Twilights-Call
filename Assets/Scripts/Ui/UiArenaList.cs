using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiArenaList : UiScreen
{
    public List<ArenaButton> m_ArenaTabs;
    // Start is called before the first frame update
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();

        ResetCursorPosition();
        
        
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SelectArena();
        m_MenuControls.Disable();
    }


    public override void OnPop()
    {
        base.OnPop();
        m_MenuControls.Disable();
    }

    

    public override void OnPush()
    {
        ResetCursorPosition();
        m_ArenaTabs[0].ChangeColorToSelected();
        base.OnPush();
    }

    public override void ResetCursorPosition()
    {
        m_CursorYMax = m_ArenaTabs.Count - 1;
        m_CursorYCurrent = 0;
        m_CursorYMin = 0;
        
        m_CursorXMax = 0;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;

        foreach (ArenaButton arena in m_ArenaTabs)
        {
            arena.ChangeColorToDefault();
        }
        m_ArenaTabs[m_CursorYCurrent].ChangeColorToSelected();    
    }

    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_ArenaTabs[m_CursorYCurrent].ChangeColorToSelected();        
        m_ArenaTabs[m_CursorYPrevious].ChangeColorToDefault();

    }


    public void SelectArena()
    {
        SceneManager.LoadScene(2);
        //GameManager.Instance.CombatManager.m_GridFormation = m_ArenaTabs[m_CursorYCurrent].m_Arena;
      //  GameManager.Instance.SwitchToBattle();
        OnPop();
        
    }
}
