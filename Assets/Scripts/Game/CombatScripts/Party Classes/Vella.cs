using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vella : Ally
{
   

    // Use this for initialization
    public override void Initialize ()
    {
        CurrentHealth = 50;
        MaxHealth = 50;
        Strength = 75;
        Magic = 40;
        Hit = 20;
        Evasion = 20;
        Defence = 20;
        Resistance = 20;
        Name = "Vella";

        AmountOfTurns = 1;

        SetCreature();
        
        m_Attack = m_CreatureSkillList.SetSkills(SkillList.SkillEnum.Attack);
        
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.HolyWater));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.ShadowBlast));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.PheonixSpirit));
        m_Skills.Add(m_CreatureSkillList.SetSkills(SkillList.SkillEnum.icerain));

        
        m_Domain = new PatchWorkChimera();
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        
        m_CreaturesMovementType = m_MovementList.ReturnMovementType(MovementList.MovementCategories.Normal);


        Model = (GameObject)Resources.Load("Objects/Battle/PartyModels/Vella/Prefab/Pref_Vella", typeof(GameObject));

        m_Texture = (Material)Resources.Load("Materials/Portrait/Material_Knight", typeof(Material));

        charactertype = Charactertype.Ally;
        elementalStrength = ElementalStrength.Wind;
        elementalWeakness = ElementalWeakness.Fire;
    }

}
