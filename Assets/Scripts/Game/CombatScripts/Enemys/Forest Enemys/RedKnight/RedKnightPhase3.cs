using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKnightPhase3 : Enemy
{

    // Use this for initialization
    void Start()
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
            Name = NameGenerator.Instance.GetName();
        }


        SetCreature();

        m_Attack = SkillList.Instance.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(SkillList.Instance.SetSkills(SkillList.SkillEnum.Attack));

        m_SkillLootTable.Add(SkillList.Instance.SetSkills(SkillList.SkillEnum.FireBall));
        m_SkillLootTable.Add(SkillList.Instance.SetSkills(SkillList.SkillEnum.HolyWater));
        m_SkillLootTable.Add(SkillList.Instance.SetSkills(SkillList.SkillEnum.Restrict));
        
        
        AmountOfTurns = 1;

        Model = (GameObject)Resources.Load("Objects/Battle/Enemy/Forest/RedKnights/Pref_RedKnight_Phase3", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_GreenSlime", typeof(Material));

        charactertype = Charactertype.Enemy;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;
    }


}