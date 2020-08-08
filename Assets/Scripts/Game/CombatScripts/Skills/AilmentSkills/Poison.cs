﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Skills
{


        // Use this for initialization
    public override void Start()
    {

        //m_ElementalType = ElementalType.Water;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillAilment = SkillAilment.Poison;
        m_Damage = 0;
        SkillName = "Poison";
        SkillDescription = "Try to Poison the enemy";
    }

    // Update is called once per frame
    void Update()
    {

    }

     public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
     {
         
         return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
             0.1f, 0.1f, 1);
         
     }
}
