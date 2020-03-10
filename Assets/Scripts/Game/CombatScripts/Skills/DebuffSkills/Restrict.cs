using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restrict : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Shadow;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 3;
        m_CostToUse = 60;
        SkillName = "Restrict";
        SkillDescription = "Make the enemies damage be weaker";
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