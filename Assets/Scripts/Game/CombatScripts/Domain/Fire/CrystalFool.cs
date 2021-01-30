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


        Addressables.LoadAssetAsync<Material>("CrystalFool").Completed += OnLoadDomainMaterial;
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
        m_CreatureOnDomain.DomainStrength =  m_CreatureOnDomain.BaseStrength / 4;
    }
    
    public override void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {
        m_CreatureOnDomain.DomainStrength = 0;
    }
}
