using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaButton : MonoBehaviour
{

    public GameObject m_Arena;
    public GridFormations m_Gridformation;

    public TextMeshProUGUI m_TextName;
    public TextMeshProUGUI m_TextMission;
    public TextMeshProUGUI m_TextDescription;

    public void Start()
    {
        m_Gridformation = m_Arena.GetComponentInChildren<GridFormations>();
        
        m_TextName.text = m_Gridformation.m_ArenaName;
        m_TextMission.text = m_Gridformation.m_MissionTag;
        m_TextDescription.text = m_Gridformation.m_Description;
    }
}
