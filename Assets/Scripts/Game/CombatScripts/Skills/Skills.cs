using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Skills
{

    // Use this for initialization


    public enum ElementalType
    {
        Fire,
        Ice,
        Light,
        Shadow,
        Wind,
        Earth,
        Lightning,
        Null
    }

    public enum SkillType
    {
        Attack,
        Heal,
        Defence,
        Domain,

    }
    public enum DamageType
    {
        Strength,
        Magic

    }

    public enum SkillFormation
    {
        SingleNode,
        FourInAStraightLine,
        BrickOf4,
    }

    public enum SkillAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }

    [SerializeField]
    public ElementalType m_ElementalType;
    [SerializeField]
    public SkillType m_SkillType;
    [SerializeField]
    public SkillFormation m_SkillFormation;
    [SerializeField]
    public DamageType m_Damagetype;
    [SerializeField]
    public SkillAilment m_SkillAilment;

    [SerializeField]
    public string SkillName;
    [SerializeField]
    public string SkillDescription;

    [SerializeField]
    public ParticleSystem m_SkillParticleEffect;

    [SerializeField]
    public int m_CostToUse;

    [SerializeField]
    public int m_Damage;

    [SerializeField]
    public string m_AnimationName;

    
    [SerializeField]
    public int m_SkillRange;

    public virtual void Start()
    {
        m_SkillRange = 2;
    }
    public virtual void Update()
    {

    }
    public ParticleSystem GetSkillParticleEffect()
    {
        return m_SkillParticleEffect;
    }

    public int GetCostToUse()
    {
        return m_CostToUse;
    }

    public string GetSkillName()
    {
        return SkillName;
    }

    public string GetSkillDescription()
    {
        return SkillDescription;
    }

    public DamageType GetDamageType()
    {
        return m_Damagetype;
    }

    public ElementalType GetElementalType()
    {
        return m_ElementalType;
    }

    public SkillAilment GetAlimentType()
    {
        return m_SkillAilment;
    }

    public SkillType GetSkillType()
    {
        return m_SkillType;
    }

    public SkillFormation GetSkillRange()
    {
        return m_SkillFormation;
    }

    public virtual IEnumerator UseSkill(Creatures aVictum, Creatures aAttacker )
    {
        return null;
    }

    public int GetSkillDamage()
    {
        return m_Damage;
    }

}
