using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : Singleton<LevelCreator>
{
    // Start is called before the first frame update

    public enum MapEditorMode
    {
        None,
        Prop,
        Enemy,
        Terrain,
        NodeReplacement
    }



   public  PropList m_PropList;
   public Node.NodeTypes m_NodeType;
   public CombatNode.CombatNodeTypes m_CombatNodeTypes;
   public PropList.NodeReplacements m_NodeReplacements;
   public PropList.Props m_PropIndex;
   public MapEditorMode m_LeftClickState;
   public MapEditorMode m_RightClickState;
   public  GameObject m_Selector;
   private CombatNode m_CurrentNode;
   private int layerMask;
    
    public void StartEditor()
    {
        m_PropList = new PropList();
        m_PropList.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
