using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skills
{
    public override void Start()
    {

        m_ElementalType = ElementalType.Fire;
        m_SkillAilment = SkillAilment.Poison;
        m_SkillType = SkillType.Attack;
        m_SkillFormation = SkillFormation.SingleNode;
        m_Damagetype = DamageType.Magic;
        m_SkillParticleEffect = (ParticleSystem)Resources.Load("ParticleSystems/Waves/FireWave/ParticleEffect_FireWave", typeof(ParticleSystem));
        m_CostToUse = 40;
        m_Damage = 10;
        SkillName = "Fire Ball";
        SkillDescription = "FireBall that will hit the whole enemy team";
    }
    public override IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return aVictum.DecrementHealth(m_Damage + aAttacker.GetAllStrength(), GetElementalType(),
            0.1f, 0.1f, 1);
    }
}
