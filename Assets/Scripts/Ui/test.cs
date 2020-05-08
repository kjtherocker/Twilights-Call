using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : Singleton<test>
{
    // Start is called before the first frame update
    void Start()
    {
        CombatManager.Instance.m_Healthbar.m_CurrentHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
