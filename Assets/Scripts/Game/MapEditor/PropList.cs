using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[ExecuteInEditMode]
public class PropList
{

    public enum Props
    {
        None,

        //Trees
        Tree1,
        Tree2,
        Tree3,

        //Pillars
        Angled_Top_Broken_Pillar,
        Fully_Intact_Pillar,
        Middle_Broken_Pillar_1,
        Middle_Broken_Pillar_2,
        Middle_Dented_Pillar,
        Pillar_Stub,
        Top_Broken_Pillar,

        //Bridges
        StartBridge,
        MiddleBridge,
        EndBridge,

        //Ruin Walls

        Corner_Ruin_Wall,
        Modular_Wall_End,
        Modular_Wall_Mid1,
        Modular_Wall_Mid2,
        Modular_Wall_Mid3,
        Modular_Wall_Mid4,
        Modular_Wall_Mid5,
        Modular_Wall_Start,
        Singular_Ruin_Wall,



        //End
        NumberOfProps
    }

    public enum NodeReplacements
    {
        None,

        BridgeStart,
        BridgeMiddle,
        BridgeEnd,

        Stairs,
        

        //End
        NumberOfProps
    }



    //public List<GameObject> m_PropSet;

    private Dictionary <Props, GameObject> m_Props = new Dictionary <Props, GameObject>();

    private Props m_PropType;
    
    private List<NodeReplacement> m_NodeReplacements;
    // Start is called before the first frame update
    public void Initialize()
    {

        m_PropType = Props.Tree1;

        Debug.Log("Initialized");
        //Tree
        AddPropToDictionary(Props.Tree1,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Tree2,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Tree3,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        
        //Bridge
        
        AddPropToDictionary(Props.EndBridge,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.MiddleBridge,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.EndBridge,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        
        //Pillar
        
        AddPropToDictionary(Props.Pillar_Stub,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Fully_Intact_Pillar,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Middle_Broken_Pillar_1,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Middle_Broken_Pillar_2,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Middle_Dented_Pillar,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
       
        
        //Wall
        AddPropToDictionary(Props.Modular_Wall_End,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Corner_Ruin_Wall,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Modular_Wall_Mid1,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Modular_Wall_Mid2,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Modular_Wall_Mid3,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Modular_Wall_Mid4,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Modular_Wall_Mid5,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
            
        AddPropToDictionary(Props.Modular_Wall_Start,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));
        
        AddPropToDictionary(Props.Singular_Ruin_Wall,
            Resources.Load<GameObject>("Objects/Battle/PartyModels/Sigma/Prefab/Pref_Sigma"));

        
        
        
        //Addressables.LoadAssetAsync<GameObject>("Tree1").Completed += OnComplete;


         
        
        
        
     // StartCoroutine(LoadProps());
        m_PropType = Props.None;
    }


    public void AddPropToDictionary(Props aPropType, GameObject aGameObject)
    {
        if (m_Props.ContainsKey(aPropType) )
        {
            Debug.Log("Prop Type " + aPropType + " is already initialized");
            return;
            
        }
        
        
        

      //  m_Props.Add(Props.Tree1,aGameObject);
//
      //  Debug.Log(m_Props.Values.Count);
        
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            
           // Debug.Log("This is what testo is " + m_Testo);
        }
    }

   public IEnumerator LoadProps()
   {
    //  m_PropSet = new List<Prop>();
    //   yield return Addressables.LoadAssetsAsync<Prop>("prop", ob =>
    //   {
    //       
    //       Prop tempProp = ob.GetComponent<Prop>();
    //       
    //       m_PropSet.Add(tempProp);
    //       
    //   });
    
        yield break;
   }


    public GameObject ReturnPropData(Props aProp, string sourceName = "Global")
    {
        
        Debug.Log("Prop Found " + m_Props[aProp]);
        if (m_Props.ContainsKey(aProp))
        {
            return m_Props[aProp];
        }

        return null;
    }

    public NodeReplacement NodeReplacementData(NodeReplacements aProp, string sourceName = "Global")
    {
        return m_NodeReplacements[(int)aProp];
    }
}
