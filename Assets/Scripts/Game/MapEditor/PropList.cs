using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[ExecuteInEditMode]
public class PropList : Singleton<PropList>
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
    private  List<Prop> m_PropSet = new List<Prop>();
    private Dictionary <Props, GameObject> m_Props = new Dictionary <Props, GameObject>();

    private Props m_PropType;
    
    private List<NodeReplacement> m_NodeReplacements;
    // Start is called before the first frame update
    void Start()
    {

        m_PropType = Props.Tree1;
       //  m_Props.Add(Props.None, Addressables.LoadAssetAsync<GameObject>("Tree1").Completed += OnComplete);


         
        
        
        
     // StartCoroutine(LoadProps());
        m_PropType = Props.None;
    }


    private GameObject OnComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("This is what addressables is getting " + obj.Result);
        return obj.Result;
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
       m_PropSet = new List<Prop>();
        yield return Addressables.LoadAssetsAsync<Prop>("prop", ob =>
        {
            
            Prop tempProp = ob.GetComponent<Prop>();
            
            m_PropSet.Add(tempProp);
            
        });
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
