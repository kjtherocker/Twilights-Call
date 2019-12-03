using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTest : Singleton<EditorTest>
{
    public CombatNode.CombatNodeTypes m_NodeType;
    public PropList.Props m_Props;
    [SerializeField]
    public GameObject m_Selector;
    public EnemyList.EnemyEnum m_Enemys;
}
