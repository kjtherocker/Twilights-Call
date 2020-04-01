using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DomainClashTabs : MonoBehaviour
{

  public GameObject m_SlidingTab;
  public TextMeshProUGUI m_TerritoryText;
  public Image m_Portrait;
  public TextMeshProUGUI m_NameTag;
  public int TerritoryValue;
  public int MaxTerritoryValue;
  
  public void SetCreature(Creatures aCreature)
  {
    m_NameTag.text = aCreature.name;
    TerritoryValue = 0;
    MaxTerritoryValue = aCreature.m_CreatureAi.m_NodeInDomainRange.Count;
    
  }
  
  public void IncrementTerritoryNumbers()
  {
    if (TerritoryValue == MaxTerritoryValue)
    {
      return;
    }
    TerritoryValue++;
    m_TerritoryText.text = TerritoryValue.ToString();
  }
  
}
