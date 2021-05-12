using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigma : Ally {


   

	// Use this for initialization
	public override void Initialize ()
    {
	    MaxHealth = 200;
        CurrentHealth = MaxHealth;
        
        BaseStrength = 34;
        BaseMagic = 76;
        BaseHit = 20;
        BaseEvasion = 20;
        BaseDefence = 20;
        BaseResistance = 20;
        m_Name = "Sigma";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 8;
		
        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);

        m_DomainList = DomainList.DomainListEnum.PatchworkChimera;
        
        m_Domain = new PatchWorkChimera();
        m_Domain.Start();
        m_Domain.DomainUser = m_Name;
        m_Domain.m_Creature = this;

        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Normal);
        
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Invigorate));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.FireBall));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma", typeof(GameObject));
        
        m_PortaitMaterial = (Material)Resources.Load("Objects/Overworld/Textures/Portraits/Material_Portrait_Sigma", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;

    }
	
	// Update is called once per frame

}
