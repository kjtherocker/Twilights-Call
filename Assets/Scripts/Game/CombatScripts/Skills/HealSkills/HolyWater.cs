﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Skills
{


    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Heal;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 300;
        m_CostToUse = 60;
        SkillName = "Holy Water";
        SkillDescription = "Heals the whole party a small amount";

    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
            0.1f, 0.1f, 1);
        
    }
}
