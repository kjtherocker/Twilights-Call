using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiArenaList : UiScreen
{
    public List<ArenaButton> m_ArenaTabs;
    // Start is called before the first frame update
    public override void Initialize()
    {

    }


    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SelectArena();
        }
    }

    public void SelectArena()
    {
        GameManager.Instance.CombatManager.m_GridFormation = m_ArenaTabs[0].m_Arena;
        GameManager.Instance.SwitchToBattle();
        OnPop();
        
    }
}
