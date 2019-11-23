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

    void OnEnable()
    {
        // Setup the SerializedProperties.
        m_NodeType = serializedObject.FindProperty("m_NodeType");
        
    }
    // Update is called once per frame
    void OnSceneGUI()
    {
        //Debug.Log("GUI is being hit");
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        Ray worldRay1 = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hitInfo1;
        if (Physics.Raycast(worldRay1, out hitInfo1, 10000))
        {
          //  (hitInfo1.transform.gameObject.GetComponent<CombatNode>());
        }
        
        switch (Event.current.GetTypeForControl(controlID))
        {

            case EventType.MouseDown:
                GUIUtility.hotControl = controlID;
                Debug.Log("MouseDown");
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(worldRay, out hitInfo, 10000))
                {
                    
                    SwitchNodeReplacement(hitInfo.transform.gameObject.GetComponent<CombatNode>());
                }

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
            serializedObject.Update();
            m_NodeType = serializedObject.FindProperty("m_NodeType");
            
            Debug.Log(aCombatnode + " we changed this into " + (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex);

            aCombatnode.m_CombatsNodeType = (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex;
            aCombatnode.SetPropState();
            Debug.Log(aCombatnode.m_CombatsNodeType);
        }

        void SelectorPosition(CombatNode aCombatnode)
        {
            serializedObject.Update();
          // m_Selector = serializedObject.FindProperty("m_Selector");
          // m_Selector..gameObject.transform.position =
          //     new Vector3(aCombatnode.transform.position.x, aCombatnode.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, aCombatnode.transform.position.z);
        }


        void SwitchNodeReplacement(CombatNode aCombatnode)
        {
            serializedObject.Update();
            m_Prop = serializedObject.FindProperty("m_Props");
            aCombatnode.m_PropOnNode = (PropList.Props)m_Prop.enumValueIndex;;
            aCombatnode.SetPropState();
        }

}
