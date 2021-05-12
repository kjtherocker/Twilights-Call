using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fide : Ally
{
    // Use this for initialization
    public override void Initialize ()
    {
        MaxHealth = 200;
        CurrentHealth = MaxHealth;
        BaseStrength = 100;
        BaseMagic = 3;
        BaseHit = 10;
        BaseEvasion = 5;
        BaseDefence = 34;
        BaseResistance = 14;
        m_Name = "Fide";

        AmountOfTurns = 1;


        SetCreature();

        CurrentDomainpoints = 0;
                
        m_Domain = new CrystalFool();
        m_Domain.Start();
        m_Domain.DomainUser = m_Name;
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

        m_PortaitMaterial = (Material)Resources.Load("Objects/Overworld/Textures/Portraits/Material_Portrait_Fide", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
}
