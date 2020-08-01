using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor( typeof( MapEditor ) )]
public class MapEditor : Editor
{



    SerializedProperty m_Selector;
    private SerializedProperty m_PropList;
    SerializedProperty m_EditorProp;
    SerializedProperty m_EditorNode;
    SerializedProperty m_LeftClickState;
    SerializedProperty m_RightClickState;
    private SerializedProperty m_NodeType;
    SerializedProperty m_NodeReplacements;
    SerializedProperty m_Prop;
    private CombatNode m_CurrentNode;
    private int layerMask;
    
    
    void OnEnable()
    {

  
    }
//  // Update is called once per frame
//  public override void OnInspectorGUI()
//  {
//      DrawDefaultInspector();

//      MapEditor myScript = (MapEditor)target;
//      if (GUILayout.Button("Build Grid"))
//      {
//          myScript.StartEditor();
//      }


//  }
//  void OnSceneGUI()
//  {
//      int controlID = GUIUtility.GetControlID(FocusType.Passive);

//      Ray SelectorRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
//      RaycastHit RayHitinfo;
//                  
//      layerMask = 1 << 12;
//      layerMask = ~layerMask;
//      if (Physics.Raycast(SelectorRay, out RayHitinfo, 100000,layerMask))
//      {
//          
//          if ( RayHitinfo.transform.gameObject.GetComponent<CombatNode>() != null)
//          {
//              if (m_CurrentNode != RayHitinfo.transform.gameObject.GetComponent<CombatNode>())
//              {
//                  SelectorPosition(RayHitinfo.transform.gameObject.GetComponent<CombatNode>());
//              }
//          }
//      }

//      var e = Event.current;
//      switch (Event.current.GetTypeForControl(controlID))
//      {

//          case EventType.MouseDown:
//              if (e.type == EventType.MouseDown && e.button == 0)
//              {
//                  
//                  
//                  serializedObject.Update();
//                  m_LeftClickState = serializedObject.FindProperty("m_LeftClick");


//                  
//                  NodeEditor(m_CurrentNode,(GridFormationEditor.MapEditorMode)m_LeftClickState.enumValueIndex);

//              }
//              if (e.type == EventType.MouseDown && e.button == 1)
//              {
//                  serializedObject.Update();
//                  m_RightClickState = serializedObject.FindProperty("m_RightClick");
//                  
//                  NodeEditor(m_CurrentNode,(GridFormationEditor.MapEditorMode)m_RightClickState.enumValueIndex);
//              }
//              GUIUtility.hotControl = controlID;
//              Event.current.Use();
//              break;
//              

//              

//          case EventType.MouseUp:
//              GUIUtility.hotControl = 0;
//              Event.current.Use();
//              break;
//      }
//  }


//  void NodeEditor(CombatNode aCombatnode, GridFormationEditor.MapEditorMode aMapEditorMode)
//  {

//      if (aMapEditorMode == GridFormationEditor.MapEditorMode.Enemy)
//      {
//          PlaceEnemy(aCombatnode);
//      }
//      if (aMapEditorMode == GridFormationEditor.MapEditorMode.Node)
//      {
//          SwitchNodeType(aCombatnode);
//      }
//      if (aMapEditorMode == GridFormationEditor.MapEditorMode.Prop)
//      {
//          SwitchProp(aCombatnode);
//      }
//      if (aMapEditorMode == GridFormationEditor.MapEditorMode.NodeReplacement)
//      {
//          SwitchNodeType(aCombatnode);
//      }
//  }

//  void SwitchNodeType(CombatNode aCombatnode)
//      {
//          if (aCombatnode == null)
//          {
//              return;
//          }

//          serializedObject.Update();
//          m_NodeType = serializedObject.FindProperty("m_NodeType");
//          
//          Debug.Log(aCombatnode + " we changed this into " + (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex);

//          aCombatnode.m_CombatsNodeType = (CombatNode.CombatNodeTypes)m_NodeType.enumValueIndex;
//          aCombatnode.SetPropState();
//          Debug.Log(aCombatnode.m_CombatsNodeType);
//          serializedObject.Update();
//          serializedObject.ApplyModifiedProperties();
//      }

//      void SelectorPosition(CombatNode aCombatnode)
//      {
//          if (aCombatnode == null)
//          {
//              return;
//          }
//          serializedObject.Update();
//          aCombatnode.EditorSelector();

//          m_CurrentNode = aCombatnode;
//      }


//      void SwitchProp(CombatNode aCombatnode)
//      {
//          if (aCombatnode == null)
//          {
//              return;
//          }
//          serializedObject.Update();
//          m_Prop = serializedObject.FindProperty("m_Props");
//          aCombatnode.m_PropOnNode = (PropList.Props)m_Prop.enumValueIndex;;
//          aCombatnode.SetPropState();
//          serializedObject.Update();
//          serializedObject.ApplyModifiedProperties();
//      }


//      
//      void PlaceEnemy(CombatNode aCombatnode)
//      {
//          if (aCombatnode == null)
//          {
//              return;
//          }
//         serializedObject.Update();
//         aCombatnode.SpawnEnemy();
//          serializedObject.Update();
//          serializedObject.ApplyModifiedProperties();
//      }

}
