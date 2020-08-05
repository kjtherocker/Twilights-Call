using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PatchWorkChimera : Domain
{
   
    public override void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        m_SkillType = SkillType.Domain;


        m_SkillRange = 4;
        SkillName = "Patchwork Chimera";
        SkillDescription = "Steal the effect of another Domain";
        DomainUser = "";
        Domaintype = DomainType.OneTime;
        m_CostToUse = 5;
        
        
        
        Addressables.LoadAssetAsync<Material>("PatchWorkChimera").Completed += OnLoadDomainMaterial;
        
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
