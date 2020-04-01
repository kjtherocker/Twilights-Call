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
        
        
        m_DomainClashTabA.IncrementTerritoryNumbers();
        m_DomainClashTabB.IncrementTerritoryNumbers();
        StartCoroutine(ClashSlider());
    }

    public IEnumerator ClashSlider()
    {
        int CurrentTerritoryA = m_DomainClashTabA.TerritoryValue;
        int CurrentTerritoryB = m_DomainClashTabB.TerritoryValue;
        
        Debug.Log("Territory A " + CurrentTerritoryA);
        Debug.Log("Territory B " + CurrentTerritoryB);
        
      // if (CurrentTerritoryA == CurrentTerritoryB)
      // {
      //     SliderPosition = 0.5f;
      // }

        if (CurrentTerritoryA > CurrentTerritoryB)
        {
            SliderPosition += 0.1f;
        }
        if (CurrentTerritoryA < CurrentTerritoryB)
        {
            SliderPosition -= 0.1f;
        }

        if (SliderPosition <= 0.0f)
        {
            Debug.Log("BWins");
            yield break;
        }
        else if (SliderPosition >= 1.0f)
        {
            Debug.Log("AWins");
            yield break;
        }

        m_DomainClashTabA.IncrementTerritoryNumbers();
        m_DomainClashTabB.IncrementTerritoryNumbers();

        m_TerritorySlider.value = SliderPosition;
        
        
        yield return new WaitForSeconds(.5f);
        StartCoroutine(ClashSlider());
    }

    public void DomainClashWinner(Creatures aDomainWinner,Creatures  aDomainLoser)
    {
        int WinnersRemainingTerritory = aDomainWinner.m_CreatureAi.m_NodeInDomainRange.Count - aDomainLoser.m_CreatureAi.m_NodeInDomainRange.Count;
        
        

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
