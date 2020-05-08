using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMenu : UiTabScreen
{
    
    public GameObject m_DebugItems;
    
    public TextMeshProUGUI m_NodePositionText;
    public TextMeshProUGUI m_NodeType;
    public TextMeshProUGUI m_NodeProp;
    public TextMeshProUGUI m_NodeHeuristic;
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            m_DebugItems.SetActive(true);
        }
    }
}
