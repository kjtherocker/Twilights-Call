using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CrystalFool : Domain
{
   
    public override void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        m_SkillType = SkillType.Domain;


        m_SkillRange = 4;
        SkillName = "CrystalFool";
        SkillDescription = "Increased Physical Attack damage";
        DomainUser = "";
        Domaintype = DomainType.OneTime;
        m_CostToUse = 5;
        
        
        
        Addressables.LoadAssetAsync<Material>("CrystalFool").Completed += OnLoadDomainMaterial;
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
        //   m_CreatureOnDomain.MaxHealth = m_CreatureOnDomain.MaxHealth / 2;
        m_CreatureOnDomain.CurrentHealth = m_CreatureOnDomain.CurrentHealth / 2;
    }
    
    public override void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {

    }
}
