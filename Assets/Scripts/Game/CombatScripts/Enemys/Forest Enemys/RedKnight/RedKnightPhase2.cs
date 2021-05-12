using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase2 : Enemy
{

    // Use this for initialization
    public override void Initialize ()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        BaseStrength = 75;
        BaseMagic = 40;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        if (m_Name == "No Name")
        {
            m_Name = GameManager.Instance.m_NameGenerator.GetName();
            transform.name = m_Name;
        }


        SetCreature();

        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack));

        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Restrict));
        

        AmountOfTurns = 1;
        
        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Normal);


        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase2", typeof(GameObject));

        m_PortaitMaterial = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }


}