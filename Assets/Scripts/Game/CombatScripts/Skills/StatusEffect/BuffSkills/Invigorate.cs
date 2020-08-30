using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invigorate : StatusEffects
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        SkillName = "Invigorate";
        SkillDescription = "A buff that greatly increases damage";
    }
    

    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {

        Length = 1;

        aVictum.BuffStrength = aVictum.Strength / 4;

        ActivatedCreature = aVictum;
        
        return aVictum.SetStatusEffect(this);
        
    }

    public override void RevertStatusEffect()
    {
        ActivatedCreature.BuffStrength = 0;

    }

}
