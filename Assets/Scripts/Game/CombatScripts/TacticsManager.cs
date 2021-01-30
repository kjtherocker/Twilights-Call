using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using UnityEngine.AddressableAssets;


public class TacticsManager : Singleton<TacticsManager>
{

    public PartyManager PartyManager;
    public Grid m_Grid;


    bool WhichSidesTurnIsIt;
    bool CombatHasStarted;

    private int m_EnemyAiCurrentlyInList;
    

    public Vector3 CreatureOffset;

    private GridFormations m_Gridformation;

    public HealthBar m_Healthbar;
    

    public TextMeshProUGUI m_TurnSwitchText;
    

    public List<Creatures> DeadAllys;
    public List<Creatures> TurnOrderAlly;
    public List<Creatures> TurnOrderEnemy;
    public List<Relic> Relics;

    public List<Memoria> m_MemoriaPool;

    private GameObject m_MemoriaPrefab;
    
    
    public Dictionary<Creatures, Creatures> m_CreaturesWhosDomainHaveClashed;
    

    public enum CombatStates
    {
        NoTurn,
        Spawn,
        EnemyTurn,
        AllyTurn,
        DomainClash,
    
        EndOfCombat


    }

    public CombatStates m_BattleStates;

    void Start()
    {
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        
        PartyManager = PartyManager.Instance;
    }

    public void CombatStart(GridFormations aGridFormations)
    {

         m_Grid = Grid.Instance;
        
         m_Gridformation = aGridFormations;

         PartyManager = PartyManager.Instance;
         
         GridFormations tempGridFormations = m_Gridformation;
         m_Grid.Convert1DArrayto2D(tempGridFormations.m_ListToConvert, tempGridFormations.m_GridDimensions);
         Relics = tempGridFormations.m_RelicsInGrid;
         
         TurnOrderEnemy = tempGridFormations.m_EnemysInGrid;

         foreach (Creatures aEnemys in TurnOrderEnemy)
         {
             AddHealthbar(aEnemys);
         }
         
         
        AddCreatureToCombat(PartyManager.m_CurrentParty[0], new Vector2Int(3, 2), TurnOrderAlly);
        
        AddCreatureToCombat(PartyManager.m_CurrentParty[1], new Vector2Int(3, 6), TurnOrderAlly);
        
        AddCreatureToCombat(PartyManager.m_CurrentParty[2], new Vector2Int(17, 16), TurnOrderAlly);
                                                                            
        AddCreatureToCombat(PartyManager.m_CurrentParty[3], new Vector2Int(12, 5), TurnOrderAlly);
         
        m_Gridformation.InitializeEnemys();
        CombatHasStarted = true;

        m_BattleStates = CombatStates.AllyTurn;
         
         WhichSidesTurnIsIt = false;

         if (tempGridFormations.m_StartDialogueTrigger != null)
         {
           //  tempGridFormations.m_StartDialogueTrigger.TriggerDialogue();
         }

         m_CreaturesWhosDomainHaveClashed = new Dictionary<Creatures, Creatures>();
         
         Addressables.LoadAssetAsync<GameObject>("Memoria").Completed += OnLoadMemoria;
    }


    public void InvokeSkill(IEnumerator aSkill)
    {
        StartCoroutine(aSkill);
    }
    
    public void EndTurn()
    {
        StartCoroutine(EnemyTurn());

    }



    public void AddCreatureToCombat(Creatures aCreature, Vector2Int aPosition, List<Creatures> aList)
    {
        if (aCreature == null)
        {
            Debug.Log("Creature does not exist Position at " + aPosition.ToString());
            return;

        }

        aList.Add(aCreature);

        int TopElement = aList.Count - 1;


        
        //Model
        
        aList[TopElement].ModelInGame = Instantiate<GameObject>(aList[TopElement].Model);
        aList[TopElement].ModelInGame.transform.position = m_Grid.GetNode(aPosition.x, aPosition.y).gameObject.transform.position + CreatureOffset;
        aList[TopElement].ModelInGame.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);
        
        
        //Ai
        aList[TopElement].m_CreatureAi = aList[aList.Count - 1].ModelInGame.GetComponent<AiController>();
        aList[TopElement].m_CreatureAi.m_Position = aPosition;
        aList[TopElement].m_CreatureAi.m_Grid = m_Grid;
        aList[TopElement].m_CreatureAi.m_Movement = aCreature.m_CreatureMovement;
        aList[TopElement].m_CreatureAi.m_MovementType = aCreature.m_CreaturesMovementType;
        aList[TopElement].m_CreatureAi.m_Creature = aList[aList.Count - 1];
        aList[TopElement].m_CreatureAi.Initialize();
        
        //Healthbar

        aList[TopElement].m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aList[TopElement].m_CreatureAi.transform);
        AddHealthbar(aList[TopElement]);
        

        //Node
        m_Grid.GetNode(aPosition.x, aPosition.y).m_CreatureOnGridPoint = aList[TopElement];
        m_Grid.GetNode(aPosition.x, aPosition.y).m_IsCovered = true;
        
    }


    public void AddHealthbar(Creatures aCreature)
    {

        if (aCreature == null)
        {
            Debug.Log("Creature Was null");
            return;
        }

        aCreature.m_CreatureAi.m_Healthbar = Instantiate<HealthBar>(m_Healthbar, aCreature.m_CreatureAi.transform);
        aCreature.m_CreatureAi.m_Healthbar.Partymember = aCreature;
    }


    void Update()
    {


       switch (m_BattleStates)
       {
           case CombatStates.Spawn:
             
                   m_BattleStates = CombatStates.AllyTurn;


             
               break;

           case CombatStates.AllyTurn:

               if (Input.GetKeyDown("i"))
               {
                   StartCoroutine(EnemyTurn());
               }

                break;

            case CombatStates.EnemyTurn:



                break;



            case CombatStates.EndOfCombat:

           //    if (Input.anyKey)
            //   {
              //    CombatEnd();
             //  }
               break;
       }


       if (Input.GetKeyDown("l"))
       {

       //   StartCoroutine(
       //       DomainHasClashed(PartyManager.m_CurrentParty[0], PartyManager.m_CurrentParty[1]));

       }
    }

    
    public void OnLoadMemoria(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        m_MemoriaPrefab = obj.Result;
        SpawnMemoriaPool(m_MemoriaPrefab.GetComponent<Memoria>());

    }

    public void SpawnMemoriaPool(Memoria aMemoria)
    {
        for (int i = 0; i < 10; i++)
        {
            m_MemoriaPool.Add(Instantiate(aMemoria));
            m_MemoriaPool[i].Initialize();
        }
    }

    public Memoria ReturnMemoria()
    {
        Memoria TempMemoria = null;
        
        foreach (Memoria aMemoria in m_MemoriaPool)
        {
            if (aMemoria.InUse == false)
            {
                TempMemoria = aMemoria;
            }
        }

        if (TempMemoria != null)
        {
            m_MemoriaPool.Add(Instantiate(m_MemoriaPrefab.GetComponent<Memoria>()));
            TempMemoria =  m_MemoriaPool[m_MemoriaPool.Count - 1];
        }

        TempMemoria.InUse = true;
        return TempMemoria;
    }

    public void SetDomainClash(Creatures CreatureA, Creatures CreaturesB)
    {
        if (m_CreaturesWhosDomainHaveClashed.ContainsKey(CreatureA) &&
            m_CreaturesWhosDomainHaveClashed.ContainsValue(CreaturesB))
        {
            return;
        }
        
        if (m_CreaturesWhosDomainHaveClashed.ContainsKey(CreaturesB) &&
            m_CreaturesWhosDomainHaveClashed.ContainsValue(CreatureA))
        {
            return;
        }
        
        

        
        Debug.Log("We are Clashing " + CreatureA.name + " " + CreaturesB.name );
        m_CreaturesWhosDomainHaveClashed.Add(CreatureA,CreaturesB);
    }
    
    
    public IEnumerator DomainHasClashed(Creatures CreatureA, Creatures CreaturesB)
    {
        
        m_BattleStates = CombatStates.DomainClash;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "Domain Clash";
        m_TurnSwitchText.color = Color.white;

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
        
        UiManager.instance.PushScreen(UiManager.Screen.DomainClash);
           
        UiDomainClash ScreenTemp =
            UiManager.Instance.GetScreen(UiManager.Screen.DomainClash) as UiDomainClash;

        ScreenTemp.SetClash(CreatureA, CreaturesB);
    }

    public void RemoveDeadFromList(Creatures.Charactertype aCharactertype)
    {
        if (aCharactertype == Creatures.Charactertype.Ally)
        { 
            for (int i = TurnOrderAlly.Count - 1; i >= 0; i--)
            {
                if (TurnOrderAlly[i] == null)
                {    
                    TurnOrderAlly.RemoveAt(i);
                }
            }
        }



    }

    public IEnumerator EnemyTurn()
    {
        m_BattleStates = CombatStates.EnemyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "ENEMY TURN";
        m_TurnSwitchText.color = Color.red;

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
        
        m_EnemyAiCurrentlyInList = 0;
        EnemyMovement();

    }

    public void EnemyMovement()
    {

        if (m_EnemyAiCurrentlyInList > TurnOrderEnemy.Count - 1)
        {
            StartCoroutine(AllyTurn());
            return ;
        }
        else
        {
            EnemyAiController EnemyTemp = TurnOrderEnemy[m_EnemyAiCurrentlyInList].m_CreatureAi as EnemyAiController;
            m_EnemyAiCurrentlyInList++;
            if (EnemyTemp.DoNothing == false)
            {
                m_Grid.RemoveWalkableArea();
                EnemyTemp.EnemyMovement();
            }
        }
    }

    public void CombatEnd()
    {
        
      //  UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
    }


    public IEnumerator AllyTurn()
    {
        m_BattleStates = CombatStates.AllyTurn;

        m_TurnSwitchText.gameObject.SetActive(true);
        m_TurnSwitchText.text = "PLAYER TURN";
        m_TurnSwitchText.color = Color.blue;


        foreach (Creatures creature in TurnOrderAlly)
        {
            creature.m_CreatureAi.m_HasMovedForThisTurn = false;
            creature.m_CreatureAi.m_HasAttackedForThisTurn = false;
            creature.EndTurn();
        }

        yield return new WaitForSeconds(2f);
        m_TurnSwitchText.gameObject.SetActive(false);
    }
    

}
