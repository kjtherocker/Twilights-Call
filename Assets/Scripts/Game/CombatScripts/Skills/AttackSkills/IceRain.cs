﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRain : Skills
{



    // Use this for initialization
    public override void Start()
    {

        m_ElementalType = ElementalType.Ice;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_Damage = 5;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/IceWave/ParticleEffect_IceWave", typeof(ParticleSystem));
        SkillName = "Ice Rain";
        SkillDescription = "IceRain that will hit the whole enemy team";
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