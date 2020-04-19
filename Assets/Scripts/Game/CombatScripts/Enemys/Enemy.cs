using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creatures
{

    public List<Skills> m_SkillLootTable;
    public override void Death()
    {
        base.Death();
        Grid.Instance.GetNode(m_CreatureAi.m_Position.x, m_CreatureAi.m_Position.y).m_CreatureOnGridPoint = null;

        Grid.Instance.GetNode(m_CreatureAi.m_Position.x, m_CreatureAi.m_Position.y).m_IsCovered = false;
        
        Grid.Instance.GetNode(m_CreatureAi.m_Position.x, m_CreatureAi.m_Position.y).SpawnMemoria(m_SkillLootTable);
        
        Destroy(gameObject);
    }
}
