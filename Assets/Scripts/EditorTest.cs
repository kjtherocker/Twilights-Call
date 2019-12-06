using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorTest : Singleton<EditorTest>
{
    public enum MapEditorMode
    {
        Enemy,
        Prop,
        Node,
        NodeReplacement
        
    }
    
    public CombatNode.CombatNodeTypes m_NodeType;
    public PropList.Props m_Props;
    [SerializeField]
    public GameObject m_Selector;
    public EnemyList.EnemyEnum m_Enemys;
    public MapEditorMode m_LeftClick;
    public MapEditorMode m_RightClick;
}
