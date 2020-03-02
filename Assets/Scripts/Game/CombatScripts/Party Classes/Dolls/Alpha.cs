using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha : Creatures {


   

    // Use this for initialization
    void Start ()
    {
        CurrentHealth = 50;
        MaxHealth = 50;
        CurrentMana = 100;
        MaxMana = 200 ;
        Strength = 800;
        Magic = 300;
        Dexterity = 50;
        Speed = 4;
        Name = "Alpha";

        AmountOfTurns = 1;


        SetCreature();

        m_CreatureMovement = 5;
		
        m_Attack = SkillList.Instance.SetSkills(SkillList.SkillEnum.Attack);

        m_DomainList = DomainList.DomainListEnum.PatchworkChimera;
        
        m_Domain = DomainList.Instance.SetDomain(m_DomainList);
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        
        m_Skills.Add(SkillList.Instance.SetSkills(SkillList.SkillEnum.icerain));

        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Dolls/Alpha/Pref_Alpha", typeof(GameObject));
        
        m_Texture = (Material)Resources.Load("Objects/Portrait/Material_Knight", typeof(Material));



        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Water;
        elementalWeakness = ElementalWeakness.Fire;

    }
	
    // Update is called once per frame

}
