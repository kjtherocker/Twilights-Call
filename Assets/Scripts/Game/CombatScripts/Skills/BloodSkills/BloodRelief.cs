using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRelief : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Shadow;
        m_SkillType = SkillType.Defence;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/DarkWave/ParticleEffect_DarkWave", typeof(ParticleSystem));
        m_Damage = 4;
        SkillName = "Blood Relief";
        SkillDescription = "Sacrifice 25% of your current health for 25% mana gain";
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
