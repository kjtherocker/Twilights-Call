using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaButton : MonoBehaviour
{

    public GameObject m_Arena;
    public GridFormations m_Gridformation;


    public RawImage m_Background;
    public Color m_BackgroundColorDefault;
    public Color m_BackgroundColorSelected;
    public TextMeshProUGUI m_TextName;
    public TextMeshProUGUI m_TextMission;
    public TextMeshProUGUI m_TextDescription;

    public void Start()
    {
        if (m_Arena != null)
        {
            m_Gridformation = m_Arena.GetComponentInChildren<GridFormations>();

            m_TextName.text = m_Gridformation.m_ArenaName;
            m_TextMission.text = m_Gridformation.m_MissionTag;
            m_TextDescription.text = m_Gridformation.m_Description;
        }

    }

    public void ChangeColorToDefault()
    {
        m_Background.color = m_BackgroundColorDefault;
    }

    public void ChangeColorToSelected()
    {
        m_Background.color = m_BackgroundColorSelected;
    }


}
