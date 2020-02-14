using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiArenaList : UiScreen
{
    public List<ArenaButton> m_ArenaTabs;
    // Start is called before the first frame update
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();

        m_CursorYMax = m_ArenaTabs.Count - 1;
        m_CursorYCurrent = 0;
        m_CursorYMin = 0;
        
        m_CursorXMax = 0;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;
        
        
        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.XButton.performed += XButton => SelectArena();
        m_MenuControls.Player.SquareButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Disable();
    }
    

    public void SelectArena()
    {
        GameManager.Instance.CombatManager.m_GridFormation = m_ArenaTabs[0].m_Arena;
        GameManager.Instance.SwitchToBattle();
        OnPop();
        
    }
}
