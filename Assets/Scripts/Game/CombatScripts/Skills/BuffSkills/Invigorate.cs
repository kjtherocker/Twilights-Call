using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invigorate : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Heal;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 3;
        m_CostToUse = 60;
        SkillName = "Invigorate";
        SkillDescription = "A buff that strongly increases damage";
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
