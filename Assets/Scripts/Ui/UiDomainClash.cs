using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiDomainClash : UiScreen
{
    // Use this for initialization

    
    public Slider m_TerritorySlider;

    public float m_SliderValue;

    public float TerritoryA;
    public float TerrtoryB;
    
    
    
    public override void Initialize()
    {

        m_MenuControls = new PlayerInput();

        m_SliderValue = 0.5f;
        m_TerritorySlider.value = m_SliderValue;
        m_MenuControls.Disable();
    }


    public void Update()
    {
     //   m_TerritorySlider.value = m_SliderValue;
    }

    public override void OnPop()
    {
        gameObject.SetActive((false));
        m_MenuControls.Disable();
    }

    public override void OnPush()
    {
        gameObject.SetActive((true));

        InputManager.Instance.m_MovementControls.Disable();
        ResetCursorPosition();

    }

}
