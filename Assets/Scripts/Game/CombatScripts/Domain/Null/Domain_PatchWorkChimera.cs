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
        
        m_DomainTexture = (Material)Resources.Load("Objects/Battle/Node/Material/Material_CrystalNode", typeof(Material));
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
     //   m_CreatureOnDomain.MaxHealth = m_CreatureOnDomain.MaxHealth / 2;
        m_CreatureOnDomain.MaxHealth = m_CreatureOnDomain.MaxHealth / 2;

          if (m_CreatureOnDomain.CurrentHealth >= m_CreatureOnDomain.MaxHealth)
          {
              m_CreatureOnDomain.CurrentHealth = m_CreatureOnDomain.MaxHealth;
          }
    }
    
    public override void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {
       m_CreatureOnDomain.ReturnStatsToStateAfterDomain();
    }
}
