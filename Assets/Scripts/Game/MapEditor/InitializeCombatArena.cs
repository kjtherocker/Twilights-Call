using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeCombatArena : MonoBehaviour
{
    public GridFormations m_CurrentLevel;

    public CombatCameraController m_BattleCamera;


    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_BattleCamera.InitalizeCamera();
        CombatManager.Instance.CombatStart(m_CurrentLevel);
    }
}
