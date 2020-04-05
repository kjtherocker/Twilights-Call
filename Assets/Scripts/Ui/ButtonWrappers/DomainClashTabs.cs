using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DomainClashTabs : MonoBehaviour
{

  public Creatures m_Creatures;
  public GameObject m_SlidingTab;
  public TextMeshProUGUI m_TerritoryText;
  public Image m_Portrait;
  public TextMeshProUGUI m_NameTag;
  public int TerritoryValue;
  public int MaxTerritoryValue;

  public void SetCreature(Creatures aCreature)
  {
      m_Creatures = aCreature;
      m_NameTag.text = aCreature.name;
      MaxTerritoryValue = aCreature.m_CreatureAi.m_NodeInDomainRange.Count;
      TerritoryValue = aCreature.m_CreatureAi.m_NodeInDomainRange.Count;
      m_TerritoryText.text = TerritoryValue.ToString();
  }

}
