﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fide : Ally
{
    // Use this for initialization
    void Start()
    {
        CurrentHealth = 50;
        MaxHealth = 50;
        CurrentMana = 100;
        MaxMana = 200;
        Strength = 75;
        Magic = 40;
        Hit = 20;
        Evasion = 20;
        Defence = 20;
        Resistance = 20;
        Name = "Fide";

        AmountOfTurns = 1;


        SetCreature();

        CurrentDomainpoints = 0;
                
        m_Domain = new CrystalFool();
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        m_Domain.m_Creature = this;
        
        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.ShadowBlast));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.icerain));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));

        m_BloodArts.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.BloodRelief));

        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Normal);

        
        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Fide/Pref_Fide", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
}
