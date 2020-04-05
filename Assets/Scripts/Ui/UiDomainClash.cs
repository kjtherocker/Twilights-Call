using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Ref<T>
{
    private T backing;
    public T Value {get{return backing;}}
    public Ref(T reference)
    {
        backing = reference;
    }
}

public class UiDomainClash : UiScreen
{
    // Use this for initialization

    
    public Slider m_TerritorySlider;

    public float m_SliderValue;

    public int TerritoryA;
    public float TerritoryB;
    public DomainClashTabs m_DomainClashTabA;
    public DomainClashTabs m_DomainClashTabB;
    public float SliderPosition;
    public float m_SliderSpeed;
    
    
    public override void Initialize()
    {

        m_MenuControls = new PlayerInput();

        m_SliderValue = 0.5f;
        m_TerritorySlider.value = m_SliderValue;
        m_MenuControls.Disable();
    }
    
    public void SetClash(Creatures aCreatureA, Creatures aCreatureB)
    {

        SliderPosition = 0.5f;
        
        m_DomainClashTabA.SetCreature(aCreatureA);
        m_DomainClashTabB.SetCreature(aCreatureB);


        if (m_DomainClashTabA.TerritoryValue > m_DomainClashTabB.TerritoryValue)
        {
            ClashSlider(true);
        }
        else if (m_DomainClashTabB.TerritoryValue > m_DomainClashTabA.TerritoryValue)
        {
            ClashSlider(false);
        }




    }

    public void ClashSlider(bool aWinner)
    {
        if (aWinner)
        {
            StartCoroutine(MoveSlider(0.1f));
        }
        
        if (aWinner == false)
        {
            StartCoroutine(MoveSlider(-0.1f));
        }
        
    }

    public IEnumerator MoveSlider(float aSliderDirection)
    {
        if (m_TerritorySlider.value <= 0 || m_TerritorySlider.value >= 1)
        {
            Winner();
            yield break;
        }

        m_TerritorySlider.value += aSliderDirection * m_SliderSpeed * Time.deltaTime;

        yield return new WaitForEndOfFrame();

        StartCoroutine(MoveSlider(aSliderDirection));

    }

    public void Winner()
    {
        
        if (m_DomainClashTabA.TerritoryValue > m_DomainClashTabB.TerritoryValue)
        {
            DomainClashWinner(m_DomainClashTabA.m_Creatures,m_DomainClashTabB.m_Creatures);
        }
        else if (m_DomainClashTabB.TerritoryValue > m_DomainClashTabA.TerritoryValue)
        {
            DomainClashWinner(m_DomainClashTabB.m_Creatures,m_DomainClashTabA.m_Creatures);
        }

        
    }


    public void DomainClashWinner(Creatures aDomainWinner,Creatures  aDomainLoser)
    {
         aDomainWinner.m_CreatureAi.DomainClashResult(aDomainLoser.m_CreatureAi.m_NodeInDomainRange.Count); 
         aDomainLoser.m_CreatureAi.DomainClashResult(aDomainLoser.m_CreatureAi.m_NodeInDomainRange.Count); 
         GameManager.Instance.UiManager.PopScreen();
         InputManager.Instance.m_MovementControls.Enable();
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
