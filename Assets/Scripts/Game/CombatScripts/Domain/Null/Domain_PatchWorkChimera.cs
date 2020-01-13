using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domain_PatchWorkChimera : Domain
{
   
    public override void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        DomainName = "Patchwork Chimera";
        DomainDescription = "Steal the effect of another Domain";
        DomainUser = "";
        Domaintype = DomainType.OneTime;
        m_CostToUse = 5;
        
        m_DomainTexture = new Material(Shader.Find("Standard"));
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
     //   m_CreatureOnDomain.MaxHealth = m_CreatureOnDomain.MaxHealth / 2;
          m_CreatureOnDomain.CurrentHealth = m_CreatureOnDomain.CurrentHealth / 2;
    }
    
    public override void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {
       // m_CreatureOnDomain.MaxHealth = m_CreatureOnDomain.MaxHealth * 2;
    }
}
