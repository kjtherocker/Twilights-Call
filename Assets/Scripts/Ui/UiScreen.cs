using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScreen : MonoBehaviour
{
    public bool m_InputActive;
    public PlayerInput m_MenuControls;
    public int m_CursorYMax;
    public int m_CursorYCurrent;
    public int m_CursorYMin;
    
    public int m_CursorXMax;
    public int m_CursorXCurrent;
    public int m_CursorXMin;
    
    // Use this for initialization
    public virtual void Initialize()
    {

    }

    public virtual void OnPop()
    {
        m_MenuControls.Enable();
        gameObject.SetActive(false);
    }

    public virtual void OnPush()
    {
        m_MenuControls.Enable();
        gameObject.SetActive(true);
    }
    
    public void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXCurrent = MenuDirectionCalculation(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculation(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }

    public void MenuSelection(int aCursorX, int aCursorY)
    {
        Debug.Log(aCursorX);
        Debug.Log(aCursorY);
        
    }

    public int MenuDirectionCalculation(float Axis, int aCurrent,int aMax, int aMin)
    {
        if (Axis > 0)
        {
            aCurrent++;
        }
        
        if (Axis < 0)
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
