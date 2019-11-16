using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScreen : MonoBehaviour
{
    public bool m_InputActive;
    public PlayerInput m_MenuControls;
    // Use this for initialization
    public virtual void Initialize()
    {

    }

    public virtual void OnPop()
    {
        m_InputActive = false;
        gameObject.SetActive(false);
    }

    public virtual void OnPush()
    {
        m_InputActive = true;
        gameObject.SetActive(true);
    }

    public virtual void ReturnToLastScreen()
    {
        GameManager.Instance.m_UiManager.ReturnToLastScreen();
    }
}
