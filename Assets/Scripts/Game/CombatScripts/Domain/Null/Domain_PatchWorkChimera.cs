using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domain_PatchWorkChimera : Domain
{
    // Start is called before the first frame update
    public override void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        DomainName = "Patchwork Chimera";
        DomainDescription = "Steal the effect of another Domain";
        DomainUser = "";
        
        m_CostToUse = 5;
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
        m_CreatureOnDomain.CurrentHealth = m_CreatureOnDomain.CurrentHealth / 2;
    }
}
