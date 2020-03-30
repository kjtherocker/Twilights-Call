using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DomainClashTabs : MonoBehaviour
{

  public GameObject m_SlidingTab;
  public int Territory;
  public TextMeshProUGUI m_TerritoryText;
  public Image m_Portrait;
  public TextMeshProUGUI m_NameTag;
  
  
  public void SetCreature(Creatures aCreature)
  {
    m_NameTag.text = aCreature.name;
  }
  
}
