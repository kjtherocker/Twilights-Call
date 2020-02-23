using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{

    public enum ChatBoxType
    {
        White,
        Black
    }

    public enum DialogueType
    {
        DialogueBox,
        DialogueVertical
    }

    public enum FontTypes
    {
        Arial,

        NumberOfFonts
    }

    [SerializeField] Texture[] m_TypesPortraits;
    [SerializeField] Font[] m_TypesOfFonts;

   // public List<Dialogue> m_DialogueTrigger.m_Dialogue;
    public List<GameObject> m_DialogueObjects;

    public DialogueTrigger m_DialogueTrigger;

    public DialogueType m_CurrentDialogueType;
    
    public TextMeshProUGUI m_DialogueBoxText;
    public TextMeshProUGUI m_VerticalText;
    public TextMeshProUGUI m_DisplayText;
    public Text m_DisplayName;
    public Canvas m_DialogueCanvas;

    public RawImage m_ChatBox;
    public RawImage m_Portrait;

    public OverworldCamera m_OverworldCamera;

    public GameObject m_DialogueBox;
    public GameObject m_DialogueVertical;

    public int AnimatedTextiterator;
    public string CurrentText;

    int ObjectToDestroy;
    public bool RemoveGameObject;
    public bool TextScroll;
    // Use this for initialization
    void Start()
    {
        //m_DialogueTrigger.m_Dialogue = new List<Dialogue>();
        //m_DialogueCanvas.gameObject.SetActive(false);
        TextScroll = false;
    }

    public void StartDialogue(List<Dialogue> aDialogue, DialogueType aDialogueType)
    {

        if (Constants.Constants.TurnDialogueOff == false)
        {

            //m_Sentances.Clear();

            
            
            
          //  m_DisplayText.text = "";
            m_CurrentDialogueType = aDialogueType;
            if (m_CurrentDialogueType == DialogueType.DialogueBox)
            {
                m_DialogueBox.SetActive(true);
                m_DialogueBoxText.text = "";
                m_DisplayText = m_DialogueBoxText;
                
            }
            else if (m_CurrentDialogueType == DialogueType.DialogueVertical)
            {
                m_DialogueVertical.SetActive(true);
                m_VerticalText.text = "";
                m_DisplayText = m_VerticalText;
            }

            m_OverworldCamera.gameObject.SetActive(false);
            m_DialogueCanvas.gameObject.SetActive(true);

            InputManager.Instance.m_BaseMovementControls.Disable();
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (m_DialogueTrigger.m_Dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (RemoveGameObject == true)
        {

            Destroy(m_DialogueObjects[0]);
            m_DialogueObjects.RemoveAt(0);

        }
     

        if (m_DialogueTrigger.m_Dialogue[0].m_ChatBoxType == ChatBoxType.White)
        {
          //  m_DisplayText.color = Color.white;
        }


       // SetPortrait(m_DialogueTrigger.m_Dialogue[0].m_PortraitType);
        SetFont(m_DialogueTrigger.m_Dialogue[0].m_FontTypes);


        if (m_DialogueTrigger.m_Dialogue[0].m_GameObjectToAppearInCutscene != null)
        {
            GameObject temp;
            temp = Instantiate<GameObject>(m_DialogueTrigger.m_Dialogue[0].m_GameObjectToAppearInCutscene, m_DialogueTrigger.m_Dialogue[0].m_GameobjectSpawnPoint.gameObject.transform);


            m_DialogueObjects.Add(temp);
        }
        CurrentText = m_DialogueTrigger.m_Dialogue[0].m_Sentances;
     
        AnimateText(CurrentText);
        

        m_DisplayName.text = m_DialogueTrigger.m_Dialogue[0].m_Name;

        RemoveGameObject = m_DialogueTrigger.m_Dialogue[0].DestroyGameObjectOnEndOfDialogue;
        ObjectToDestroy = m_DialogueTrigger.m_Dialogue.Count;

        m_DialogueTrigger.m_Dialogue.RemoveAt(0);
    }

    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
         //  if (m_DialogueTrigger.m_Dialogue.Count > 0)
         //  {

         //      DisplayNextSentence();

         //  }
         //  else
         //  {
         //      EndDialogue();
         //  }
        }
    }

    public void AnimateText(string strComplete)
    {

        AnimatedTextiterator = 0;
        
        TextScroll = true;


        if (m_CurrentDialogueType == DialogueType.DialogueBox)
        {
            m_DisplayText.text = "";
            m_DialogueBox.SetActive(true);
            m_DisplayText = m_DialogueBoxText;
                
        }
        else if (m_CurrentDialogueType == DialogueType.DialogueVertical)
        {
            m_DisplayText.text += ("\n" + strComplete);
        }


    }


    public void ShowTextWithParse(string strComplete)
    {
        TextScroll = false;
    }

    public void SetPortrait(Constants.Portrait aPortrait, string sourceName = "Global")
    {
        m_Portrait.texture = m_TypesPortraits[(int)aPortrait];
    }

    public void SetFont(FontTypes aFont, string sourceName = "Global")
    {
       // m_DisplayText.fontStyle = m_TypesOfFonts[(int)aFont];

    }

    public void StunPlayers()
    {
        
    }

    public void EndDialogue()
    {

    
        if (m_DialogueObjects != null)
        {
            for (int i = 0; i < m_DialogueObjects.Count; i++)
            {
                Destroy(m_DialogueObjects[i]);
                m_DialogueObjects.RemoveAt(i);
            }
        }
        m_DialogueCanvas.gameObject.SetActive(false);
        if (m_DialogueTrigger != null)
        {
            m_DialogueTrigger.DialogueIsDone = true;
        }
        m_DisplayText.text = "";
        
        m_DialogueBox.SetActive(false);
        m_DialogueVertical.SetActive(false);
        //Destroy(m_DialogueTrigger.gameObject);
        AudioManager.Instance.StopSound();
        InputManager.Instance.m_BaseMovementControls.Enable();
        m_OverworldCamera.gameObject.SetActive(true);
    }


}
