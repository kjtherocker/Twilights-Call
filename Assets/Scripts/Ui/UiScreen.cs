using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScreen : MonoBehaviour
{
    public bool m_InputActive;
    public PlayerInput m_MenuControls;
    public int m_CursorYMax;
    public int m_CursorYCurrent;
    public int m_CursorYPrevious;
    public int m_CursorYMin;
    
    public int m_CursorXMax;
    public int m_CursorXCurrent;
    public int m_CursorXPrevious;
    public int m_CursorXMin;
    
    // Use this for initialization
    public virtual void Initialize()
    {
        m_MenuControls = new PlayerInput();
    }

    public virtual void OnPop()
    {
        m_MenuControls.Disable();
        gameObject.SetActive(false);
    }

    public virtual void OnPush()
    {
        if (m_MenuControls == null)
        {
            Initialize();
        }

        m_MenuControls.Enable();
        gameObject.SetActive(true);
    }
    
    public void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;
        
        m_CursorXCurrent = MenuDirectionCalculation(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculation(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }

    public virtual void MenuSelection(int aCursorX, int aCursorY)
    {
        Debug.Log(aCursorX);
        Debug.Log(aCursorY);
        
    }

    public virtual void ResetCursorPosition()
    {
        m_CursorYMax = 0;
        m_CursorYCurrent = 0;
        m_CursorYMin = 0;
        
        m_CursorXMax = 0;
        m_CursorXCurrent = 0;
        m_CursorXMin = 0;
    }

    public int MenuDirectionCalculation(float Axis, int aCurrent,int aMax, int aMin)
    {
        if (Axis < 0)
        {
            aCurrent++;
        }
        
        if (Axis > 0)
        {
            aCurrent--;
        }

        if (aCurrent < aMin)
        {
            aCurrent = aMax;
        }
        else if (aCurrent > aMax)
        {
            aCurrent = aMin;
        }

        return aCurrent;
    }

    public virtual void ReturnToLastScreen()
    {
        GameManager.Instance.m_UiManager.ReturnToLastScreen();
    }
}
