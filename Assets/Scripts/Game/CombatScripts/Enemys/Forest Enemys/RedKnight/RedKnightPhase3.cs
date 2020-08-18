using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase3 : Enemy
{

    // Use this for initialization
    public override void Initialize ()
    {
        CurrentHealth = 200;
        MaxHealth = 200;
        CurrentMana = 10;
        MaxMana = 10;
        Strength = 75;
        Magic = 40;
        Hit = 20;
        Evasion = 20;
        Defence = 20;
        Resistance = 20;
        if (Name == "No Name")
        {
            Name = GameManager.Instance.m_NameGenerator.GetName();
            transform.name = Name;
        }


        SetCreature();

        m_Attack =m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack));

        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_SkillLootTable.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Restrict));
        
        
        AmountOfTurns = 1;
        
        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Normal);


        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase3", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }


}