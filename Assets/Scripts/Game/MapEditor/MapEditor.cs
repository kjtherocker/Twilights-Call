using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( EditorTest ) )]
public class MapEditor : Editor
{
    SerializedProperty m_Selector;

    SerializedProperty m_EditorProp;
    SerializedProperty m_EditorNode;
    private SerializedProperty m_NodeType;
    SerializedProperty m_NodeReplacements;
    SerializedProperty m_Prop;
    private CombatNode m_CurrentNode;
    private int layerMask;
    void OnEnable()
    {

  
    }
    // Update is called once per frame
    void OnSceneGUI()
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        Ray SelectorRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit RayHitinfo;
                    
        layerMask = 1 << 12;
        layerMask = ~layerMask;
        if (Physics.Raycast(SelectorRay, out RayHitinfo, 100000,layerMask))
        {
            
            if ( RayHitinfo.transform.gameObject.GetComponent<CombatNode>() != null)
            {
                if (m_CurrentNode != RayHitinfo.transform.gameObject.GetComponent<CombatNode>())
                {
                    SelectorPosition(RayHitinfo.transform.gameObject.GetComponent<CombatNode>());
                }
            }
        }

        var e = Event.current;
        switch (Event.current.GetTypeForControl(controlID))
        {

            case EventType.MouseDown:
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    PlaceEnemy(m_CurrentNode);

                }
                if (e.type == EventType.MouseDown && e.button == 1)
                {
                    SwitchNodeType(m_CurrentNode);
                }
                GUIUtility.hotControl = controlID;
                Event.current.Use();
                break;
                

                

            case EventType.MouseUp:
                GUIUtility.hotControl = 0;
                Event.current.Use();
                break;
        }
    }


        void SwitchNodeType(CombatNode aCombatnode)
        {
            if (aCombatnode == null)
            {
                return;
            }

            serializedObject.Update();
            m_NodeType = serializedObject.FindProperty("m_NodeType");
            
            Debug.Log(aCombatnode + " we changed this into " + (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex);

            aCombatnode.m_CombatsNodeType = (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex;
            aCombatnode.SetPropState();
            Debug.Log(aCombatnode.m_CombatsNodeType);
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }

        void SelectorPosition(CombatNode aCombatnode)
        {
            if (aCombatnode == null)
            {
                return;
            }
            serializedObject.Update();
            aCombatnode.EditorSelector();

            m_CurrentNode = aCombatnode;
        }


        void SwitchProp(CombatNode aCombatnode)
        {
            if (aCombatnode == null)
            {
                return;
            }
            serializedObject.Update();
            m_Prop = serializedObject.FindProperty("m_Props");
            aCombatnode.m_PropOnNode = (PropList.Props)m_Prop.enumValueIndex;;
            aCombatnode.SetPropState();
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
        
        void PlaceEnemy(CombatNode aCombatnode)
        {
            if (aCombatnode == null)
            {
                return;
            }
           serializedObject.Update();
           aCombatnode.SpawnEnemy();
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }

}
